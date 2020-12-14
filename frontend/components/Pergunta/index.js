import React, { useRef, useEffect, useState } from "react";
import { View, StyleSheet, Text, Animated } from "react-native";
import {Alternativa} from '../index'
export default function Pergunta({ data, perguntaAtual, responder, resposta, readOnly }) {
  const [alternativaSelecionada, setAlternativaSelecionada] = useState(undefined)
  useEffect(() => {
    setAlternativaSelecionada(resposta)
  }, [data])

  function renderNovaAlternativaSelecionada(resposta) {
    setAlternativaSelecionada(resposta)
  }
  return (
    <View style={styles.container}>
      <Text style={styles.titulo}>{readOnly ? data.text: perguntaAtual + " - " + data.text}</Text>
      <View style={styles.containerRespostas}>
        {data.answers.map((item, index) => {
            return (
               <Alternativa 
                readOnly={readOnly}
                key={item.id}
                data={item}
                chave={item.id}
                index={index}
                perguntaAtual={perguntaAtual}
                responder={responder}
                resposta={resposta}
                correct={data.correct == item.id}
                renderNovaAlternativaSelecionada={renderNovaAlternativaSelecionada.bind(this)}
                selected={item.id == alternativaSelecionada}
               />
              );
          })}
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    justifyContent: "space-around",
    flex: 0.6,
  },
  containerRespostas: {
    justifyContent: "space-between",
  },
  
  titulo: {
    fontSize: 21,
    fontWeight: "bold",
  },
  
});
