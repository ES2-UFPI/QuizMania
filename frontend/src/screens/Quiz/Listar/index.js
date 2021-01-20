import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity, Alert } from "react-native";
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
  const [xp, setxp] = useState("");
  const [level, setlevel] = useState("");
  const [lxp, setlxp] = useState("");
  // useEffect(() => {
  //   console.log(navigation)

  // }, [navigation.isFocused()]);

  useEffect(() => {
    getData()
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

    try {
      const response = await API.obterPersonagem({})
      setlevel(response.level)
      setlxp(response.currentLevelXP)
      setxp(response.totalXP)
    } catch (error) {
      alert("Não foi possível recuperar os dados do personagem...")
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
    <Container navigation={navigation} refresh>
      <View style={{ justifyContent: "flex-start", flexDirection: 'row' , right: 50 }}>
        <Personagem stepped={step} navigation={navigation}/>
        <View style={{alignSelf: 'center', marginLeft: -20}}>
          <Text>Level XP: {lxp}</Text>
          <Text>Level: {level}</Text>
          <Text>Total XP: {xp}</Text>
        </View>
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
                  quiz: item.id,
                })
              }
            >
              <Card.Title>{item.title}</Card.Title>
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
