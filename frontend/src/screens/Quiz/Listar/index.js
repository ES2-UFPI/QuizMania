import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity } from "react-native";
import { Button, Card } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito } from "../../../../components";

export default function ListarQuizzes({navigation}) {
  const [quizzes, setQuizzes] = useState([]);
  useEffect(() => {
    const data = [
      {
        id: 1,
        questions: [
          {
            id: 1,
            text:
              "What is the answer to the meaning of life, the universe and everything?",
            hasMultipleCorrectAnswers: false,
            answers: [
              {
                id: 1,
                text: "40",
              },
              {
                id: 2,
                text: "41",
              },
              {
                id: 3,
                text: "42",
              },
              {
                id: 4,
                text: "43",
              },
            ],
            
          },
          {
            id: 2,
            text: "This is a true or false question. True or False?",
            hasMultipleCorrectAnswers: false,
            answers: [
              {
                id: 5,
                text: "True",
              },
              {
                id: 6,
                text: "False",
              },
            ],
          },
          {
            id: 3,
            text: "All options are correct. Which options are correct?",
            hasMultipleCorrectAnswers: true,
            answers: [
              {
                id: 7,
                text: "A",
              },
              {
                id: 8,
                text: "B",
              },
            ],
          },
        ],
      },
      {
        id: 2,
        questions: [
          {
            id: 3,
            text: "All options are correct. Which options are correct?",
            hasMultipleCorrectAnswers: true,
            correct: [7, 8],
            answers: [
              {
                id: 7,
                text: "A",
              },
              {
                id: 8,
                text: "B",
              },
            ],
          },
          {
            id: 2,
            text: "This is a true or false question. True or False?",
            hasMultipleCorrectAnswers: false,
            correct: [5],
            answers: [
              {
                id: 5,
                text: "True",
              },
              {
                id: 6,
                text: "False",
              },
            ],
          },
        ],
      },
    ];
    setQuizzes(data);
  }, []);

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
