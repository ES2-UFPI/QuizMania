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

  async function getData() {
    try {
      const response = await API.recuperarGuildas()
      const dadosPersonagem = await API.obterPersonagem({id: 1})
      console.log(dadosPersonagem)
      const novosDados = response.map(item => {
        console.log("rooi" , item)
        const participa = dadosPersonagem.guild && dadosPersonagem.guild.id == item.id
        return {...item, participa}

      })
      setGuildas(novosDados)
    } catch (error) {
      alert(error)
    }
  }

  async function participarGuilda(id) {
    try {
      const response = await API.participarGuilda(id)
      alert("Você agora participa dessa guilda!")
      await getData()
    } catch (error) {
      alert(error)
    }
  }


  return (
    <Container navigation={navigation} refresh>
      <Text style={{ fontSize: 30, fontWeight: "bold" }}>Guildas</Text>
      <FlatList
        data={guildas}
        renderItem={({ item, index }) => (
          <Card>
            <TouchableOpacity
              onPress={() =>{}}
            >
              <Card.Title>{item.name}</Card.Title>
            </TouchableOpacity>
            <Button onPress={() => {
              item.participa ? alert("Você já participa desta guilda") :participarGuilda(item.id)
            }} title={item.participa ? 'Participando' :'Participar'} type='clear'/>
            <Button onPress={() => {
            }} title='Detalhar' type='clear'/>
          </Card>
        )}
      />
    </Container>
  );
}
