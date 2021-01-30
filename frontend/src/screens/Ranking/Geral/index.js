import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity } from "react-native";
import { Button, Card } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito, CharacterRanking } from "../../../../components";
import API from '../../../../services'
export default function ListarQuizzes({navigation}) {
  const [characters, setCharacters] = useState([]);
  const [maior, setMaior] = useState(1);


  async function getData() {
    try {
      const response = await API.recuperarRanking()
      console.log(response.ranking)
      const xps = response.ranking.map(item => item.totalXP)
      const maiorXp = Math.max(...xps)
      setMaior(maiorXp)
      setCharacters(response.ranking)
    } catch (error) {
      alert(error)
    }
  }

  useEffect(() => {
    getData()
  }, [])


  const numColumns = 10;
  return (
    <Container navigation={navigation} >
      <FlatList
        horizontal
        data={characters}
        contentContainerStyle={{backgroundColor:'gray'}}
        style={{height: '100%', backgroundColor: 'red', flex: 1}}
        renderItem={({item, index}) => (
          <CharacterRanking porcentagem={item.totalXP/maior * 100} nome={item.name}/>
        )}
      />
    </Container>
  );
}
