import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity } from "react-native";
import { Button, Card } from "react-native-elements";
import {
  Container,
  Header,
  Pergunta,
  Gabarito,
  Personagem,
} from "../../../../components";
import API from "../../../../services";
import { Ionicons } from "@expo/vector-icons";

export default function ListarQuizzes({ navigation }) {
  const [quizzes, setQuizzes] = useState([]);
  const [step, setStep] = useState(0);
  // useEffect(() => {
  //   console.log(navigation)

  // }, [navigation.isFocused()]);

  useEffect(() => {
    const unsubscribe = navigation.addListener("focus", () => {
      getData();
      setStep(step + 1);
      console.log("chamou")
    });

    // Return the function to unsubscribe from the event so it gets removed on unmount
    return unsubscribe;
  }, [navigation]);

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
    <Container navigation={navigation}>
      <View style={{ justifyContent: "flex-start", right: 100 }}>
        <Personagem stepped={step} navigation={navigation}/>
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
