import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity, Alert } from "react-native";
import { Button, Card } from "react-native-elements";
import { Container } from "../../../../components";
import API from "../../../../services";
import { Ionicons } from "@expo/vector-icons";

export default function ListarGuildas({ navigation }) {
  const [guildas, setGuildas] = useState([]);
  const [step, setStep] = useState(0);

  useEffect(() => {
    getData();
    const unsubscribe = navigation.addListener("focus", () => {
      getData();
      setStep(step + 1);
      //console.log("chamou")
    });

    // Return the function to unsubscribe from the event so it gets removed on unmount
    return unsubscribe;
  }, [navigation]);

  async function getData() {}


  return (
    <Container navigation={navigation} refresh>
      <FlatList
        data={guildas}
        renderItem={({ item, index }) => (
          <Card>
            <TouchableOpacity
              onPress={() =>{}}
            >
              <Card.Title>{item.title}</Card.Title>
            </TouchableOpacity>
          </Card>
        )}
      />
    </Container>
  );
}
