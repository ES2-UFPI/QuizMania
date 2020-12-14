import React, { useEffect, useState } from "react";
import { View, Text, StyleSheet, TouchableOpacity } from "react-native";

export default function Gabarito({ perguntas, respostas, detalharPergunta }) {
  const [indexSelecionado, setIndexSelecionado] = useState(undefined);
  return (
    <View style={styles.container}>
      <Text style={{fontSize: 20, fontWeight: 'bold', textAlign: 'center'}}>Você acertou {"33,33%"} do quiz!</Text>
      <View style={styles.containerPerguntas}>
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
                  : item.correct == respostas[item.id.toString()]
                  ? { backgroundColor: "green" }
                  : { backgroundColor: "red" },
              ]}
            >
              <Text style={styles.texto}>{(index + 1).toString()}</Text>
            </View>
          </TouchableOpacity>
        ))}
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    width: "100%",
    paddingTop: 30
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
