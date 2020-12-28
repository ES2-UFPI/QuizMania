import React, {useState, useEffect} from "react";
import { View, StyleSheet, Image, ImageBackground, Text } from "react-native";
import { BACKGROUND_COLOR } from "../../constants";
import API from '../../services'
export default function Header() {

  const [personagem, setPersonagem] = useState({})


  useEffect(() => {
    getData();

  }, [])


  async function getData() {
    try {
      const data = await API.obterPersonagem({})
      setPersonagem(data)
    } catch (error) {
      alert(error.toString())
    }
  }

  return (
    <View style={styles.container}>
      <ImageBackground
        source={require("../../assets/images/moeda.png")}
        style={{
          width: 40,
          height: 40,
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        {personagem.gold && <Text style={styles.textImage}>{personagem.gold}</Text>}
      </ImageBackground>
      <ImageBackground
        source={require("../../assets/images/vidas.png")}
        style={{
          width: 40,
          height: 40,
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        {personagem.healthPoints &&  <Text style={styles.textImage}>{personagem.healthPoints}</Text>}
      </ImageBackground>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flexDirection: "row",
    justifyContent: "space-between",
  },
  textImage: {
    textAlign: "center",
    fontSize: 16,
    color: "white",
    fontWeight: "bold",
  },
});
