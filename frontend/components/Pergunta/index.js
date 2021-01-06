import React, { useRef, useEffect, useState } from "react";
import { View, StyleSheet, Text, Animated } from "react-native";
import { Alternativa } from "../index";
import { Button } from "react-native-elements";

export default function Pergunta({
  data,
  perguntaAtual,
  responder,
  resposta,
  readOnly,
  proximaPergunta,
  setGabaritoVisivel,
  submitRespostas,
  isCorrect,
  paramRota,
  navigation
}) {
  const [loadingResposta, setLoadingResposta] = useState(false)
  const [alternativasSelecionadas, setAlternativasSelecionadas] = useState(
    undefined
  );
  useEffect(() => {
    setAlternativasSelecionadas(resposta);
  }, [data]);

  function removeItemOnce(arr, value) {
    var index = arr.indexOf(value);
    if (index > -1) {
      arr.splice(index, 1);
    }
    return arr;
  }

  function renderNovaAlternativaSelecionada(resposta) {
    var respostasTemp;
    if (alternativasSelecionadas == undefined) respostasTemp = [];
    else respostasTemp = [...alternativasSelecionadas];
    if (data.hasMultipleCorrectAnswers) {
      if (respostasTemp.includes(resposta)) {
        respostasTemp = removeItemOnce(respostasTemp, resposta);
      } else {
        respostasTemp.push(resposta);
      }
    } else {
      if (respostasTemp.includes(resposta)) {
        respostasTemp = removeItemOnce(respostasTemp, resposta);
      } else {
        if (respostasTemp.length > 0) {
          respostasTemp[0] = resposta;
        } else {
          respostasTemp.push(resposta);
        }
      }
    }
    setAlternativasSelecionadas(respostasTemp);
    // //console.log(respostasTemp);
  }

  function mapChoices(choices) {
    return choices.map((item, index) => {
      return (
        <Alternativa
          readOnly={readOnly}
          key={item.id}
          data={item}
          chave={item.id}
          index={index}
          perguntaAtual={perguntaAtual}
          resposta={resposta}
          correct={isCorrect(item, data)}
          renderNovaAlternativaSelecionada={renderNovaAlternativaSelecionada.bind(
            this
          )}
          selected={
            alternativasSelecionadas
              ? alternativasSelecionadas.includes(item.id)
              : false
          }
        />
      );
    })
  }

  return (
    <View style={styles.container}>
      <Text style={styles.titulo}>
        {readOnly ? data.question.text : perguntaAtual + " - " + data.text}
      </Text>
      <View style={styles.containerRespostas}>
        {data.answers ? mapChoices(data.answers) : mapChoices(data.question.answers)}
      </View>
      {!readOnly && proximaPergunta ? (
        <Button
          type="outline"
          containerStyle={{ alignSelf: "center" }}
          buttonStyle={{ borderRadius: 20 }}
          title="Próxima Pergunta"
          onPress={() => {
            if (alternativasSelecionadas && alternativasSelecionadas.length > 0)
              responder(data.id, alternativasSelecionadas);
            else
              alert("Ops... Você deve selecionar pelo menos uma alternativa.");
          }}
        />
      ) : !readOnly ? (
        <Button
          title="Enviar Respostas"
          type="outline"
          containerStyle={{ alignSelf: "center" }}
          buttonStyle={{ borderRadius: 20 }}
          loading={loadingResposta}
          onPress={() => {
            if (
              alternativasSelecionadas &&
              alternativasSelecionadas.length > 0
            ) {
              responder(data.id, alternativasSelecionadas);
              try{
                submitRespostas()
                alert("Ok!")
//                navigation.navigate("Gabarito")
              } catch(error) {
                alert(error.toString())
              }
              setLoadingResposta(false)
            } else
              alert("Ops... Você deve selecionar pelo menos uma alternativa.");
          }}
        />
      ) : (<Button
        title="Continuar"
        type="outline"
        containerStyle={{ alignSelf: "center" }}
        buttonStyle={{ borderRadius: 20 }}
        loading={loadingResposta}
        onPress={() => {
          navigation.navigate("XP Ganho", {dados: paramRota})
        }}
      />
    ) }
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    justifyContent: "space-around",
    flex: 0.7,
  },
  containerRespostas: {
    justifyContent: "space-between",
    marginVertical: 10,
  },

  titulo: {
    fontSize: 21,
    fontWeight: "bold",
  },
});
