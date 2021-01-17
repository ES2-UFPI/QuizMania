import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity } from "react-native";
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
            <Ionicons
              name="md-trash"
              size={24}
              color="red"
              onPress={() => alert("Deletando quiz...")}
            />
          </Card>
        )}
      />
    </Container>
  );
}
