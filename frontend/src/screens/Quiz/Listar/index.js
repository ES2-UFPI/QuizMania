import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity, Alert } from "react-native";
import { Button, Card } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito, Personagem } from "../../../../components";
import API from '../../../../services'
import { Ionicons } from "@expo/vector-icons";

export default function ListarQuizzes({ navigation }) {
  const [quizzes, setQuizzes] = useState([]);

  useEffect(() => {
    getData();
  }, []);

  async function getData() {
    try {
      const data = await API.obterQuizzes();
      setQuizzes(data);
    } catch (error) {
      alert(error);
    }
  }

  async function deletarQuiz (id) {
    try {
      const response = await API.deletarQuiz(id)
      alert("Quiz deletado com sucesso!")
      await getData()

    } catch (error) {
      alert("Erro ao deletar o quiz.")
      console.log(error)
    }

  }

  const numColumns = 10;
  return (
    <Container navigation={navigation} >
      <View style={{justifyContent: "flex-start", right: 100}}>
      <Personagem/>
      </View>
      <Button
        onPress={() => navigation.navigate("Criar Quiz")}
        type="solid"
        title="+"
        containerStyle={{
          borderRadius: 30,
          maxHeight: 60,
          maxWidth: 60,
          position: "relative",
          top: 10,
          bottom: 0,
          left: 300,
          right: 0,
          marginTop: -50,
          zIndex: 9999,
        }}
        titleStyle={{ fontSize: 30, fontWeight: "bold" }}
      />
      <FlatList
        data={quizzes}
        renderItem={({ item, index }) => (
          <Card>
            <TouchableOpacity
              onPress={() =>
                navigation.navigate("Responder Quiz", {
                  quiz: item,
                })
              }
            >
              <Card.Title>Quiz id {item.id}</Card.Title>
            </TouchableOpacity>
            <View style={{flexDirection: 'row', justifyContent: 'space-between'}}>
            <Ionicons
              name="md-trash"
              size={24}
              color="red"
              onPress={() => {
                Alert.alert(
                  "Atenção",
                  "Deseja realmente deletar o quiz?",
                  [
                    {
                      text: "Cancelar",
                      onPress: () => console.log("Cancel Pressed"),
                      style: "cancel"
                    },
                    { text: "OK", onPress: () => deletarQuiz(item.id) }
                  ],
                );
              }}
            />
            <Ionicons
              name="md-create"
              size={24}
              color="green"
              onPress={() => navigation.navigate("Criar Quiz", {quizId: item.id})}
            />
            </View>
          </Card>
        )}
      />
    </Container>
  );
}
