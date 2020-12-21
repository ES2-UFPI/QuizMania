import React, { useEffect, useState } from "react";
import { View, Text, StyleSheet, TouchableOpacity } from "react-native";
import ResponsiveList from '../ResponsiveList'
export default function Gabarito({ perguntas, respostas, detalharPergunta }) {
  const [indexSelecionado, setIndexSelecionado] = useState(undefined);

  function isEqual(first, second) {
    if (first.length !== second.length) {
      return false;
    }
    for (let i = 0; i < first.length; i++) {
      if (!second.includes(first[i])) {
        return false;
      }
    }
    return true;
  }
  return (
    <View style={styles.container}>
<<<<<<< HEAD
      <Text style={{fontSize: 20, fontWeight: 'bold', textAlign: 'center'}}>Você acertou {"33,33%"} do quiz!</Text>
      <ResponsiveList>
=======
      <Text style={{ fontSize: 20, fontWeight: "bold", textAlign: "center" }}>
        Você acertou {"33,33%"} do quiz!
      </Text>
      <View style={styles.containerPerguntas}>
>>>>>>> develop
        {perguntas.map((item, index) => (
          <TouchableOpacity
            onPress={() => {
              detalharPergunta(item.id);
              setIndexSelecionado(index);
            }}
          >
            <View
              style={[
                styles.pergunta,
                indexSelecionado == index
                  ? { backgroundColor: "gray" }
                  : isEqual(respostas[item.id.toString()], item.correct)
                  ? { backgroundColor: "green" }
                  : { backgroundColor: "red" },
              ]}
            >
              <Text style={styles.texto}>{(index + 1).toString()}</Text>
            </View>
          </TouchableOpacity>
        ))}
      </ResponsiveList>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    width: "100%",
    paddingTop: 30,
  },
  containerPerguntas: { width: "100%", flexDirection: "row", flexWrap: "wrap" },
  pergunta: {
    alignSelf: "flex-start",
    backgroundColor: "black",
    marginVertical: 20,
    marginHorizontal: 5.5,
    padding: 10,
    height: 60,
    width: 37,
    justifyContent: "space-between",
  },
  texto: { fontSize: 30, fontWeight: "bold", color: "white" },
});
