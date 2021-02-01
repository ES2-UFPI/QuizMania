import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity } from "react-native";
import { Button, Card } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito, CharacterRanking } from "../../../../components";
import API from '../../../../services'
export default function ListarQuizzes({navigation, route}) {
  const [nome, setNome] = useState(route.params.nome)
  const [idGuilda, setidGuilda] = useState(route.params.idGuilda)

  const [characters, setCharacters] = useState([]);
  const [maior, setMaior] = useState(1);


  async function getData() {
    try {
      //console.log(idGuilda, nome)
      const response = await API.recuperarRanking(idGuilda)
      //console.log("bwq kvbevkbrwvhjjvhrbtjh",response.ranking)
      const xps = response.ranking.map(item => item.totalXP)
      const maiorXp = Math.max(...xps)
      setMaior(maiorXp)
      setCharacters(response.ranking)
      if(response.ranking.length === 0) {
        alert("Não há participantes nesta guilda...")
        navigation.goBack()
      }
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
      <Text style={{ fontSize: 30, fontWeight: "bold" }}>{nome}</Text>
      <FlatList
        horizontal
        data={characters}
        style={{height: 400, alignSelf: 'baseline',marginTop: 200}}
        renderItem={({item, index}) => (
          <CharacterRanking porcentagem={item.totalXP/maior * 100} nome={item.name} xp={item.totalXP}/>
        )}
      />
    </Container>
  );
}