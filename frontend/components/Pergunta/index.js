import React from "react";
import { View, StyleSheet, Text } from "react-native";
import { BACKGROUND_COLOR } from "../../constants";
import { Button } from "react-native-elements";

export default function Pergunta({ data, perguntaAtual, responder }) {
  return (
    <View style={styles.container}>
      <Text style={styles.titulo}>{perguntaAtual + " - " + data.titulo}</Text>
      <View style={styles.containerRespostas}>
        {Array(4)
          .fill(0)
          .map((item, index) => {
            const chave = "alternativa" + (index + 1);
            if (data[chave]) {
              return <Button title={data[chave]} buttonStyle={styles.button} onPress={() => responder(perguntaAtual, index + 1)} />;
            }
          })}
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    justifyContent: "space-around",
    flex: 0.6
  },
  containerRespostas: {
    justifyContent: "space-between",
  },
  button: {
    marginVertical: 15,
  },
  titulo: {
    fontSize: 21,
    fontWeight: 'bold'
  }
});
