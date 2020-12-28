import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity } from "react-native";
import { Button, Card } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito } from "../../../../components";
import API from '../../../../services'
export default function ListarQuizzes({navigation}) {
  const [quizzes, setQuizzes] = useState([]);



  useEffect(() => {
    getData()
  }, []);


  async function getData() {
    try {
      const data = await API.obterQuizzes();
      setQuizzes(data);  
    } catch (error) {
      alert(error)
    }
  }

  const numColumns = 10;
  return (
    <Container>
      <Header />
      <FlatList
        data={quizzes}
        renderItem={({ item, index }) => (
          <TouchableOpacity onPress={() => navigation.navigate("Responder Quiz", {
            quiz: item
          })}>
            <Card>
              <Card.Title>Quiz id {item.id}</Card.Title>
            </Card>
          </TouchableOpacity>
        )}
      />
    </Container>
  );
}
