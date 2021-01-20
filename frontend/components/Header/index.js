import React, {useState, useEffect} from "react";
import { View, StyleSheet, Image, ImageBackground, Text, AsyncStorage } from "react-native";
import { BACKGROUND_COLOR } from "../../constants";
import API from '../../services'
export default function Header({navigation}) {

  const [personagem, setPersonagem] = useState({})


  useEffect(() => {
    getData();

  }, [])


  async function getData() {
    try {
      const data = await API.obterPersonagem({})
      const dataLocal =  JSON.parse(await AsyncStorage.getItem("data"))
      let extra = 0;
      //console.log(dataLocal)
      if(dataLocal && dataLocal.vidaLocal) {
        extra = dataLocal.vidaLocal
      }
      //console.log(data)
      data.healthPoints += extra
      setPersonagem(data)
    } catch (error) {
      alert(error)
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
        {personagem && <Text style={styles.textImage}>{personagem.gold }</Text>}
      </ImageBackground>
      <Text onPress={() => {
        alert("atualizando..")
        getData()
        }}>Atualizar dados</Text>
      <ImageBackground
        source={require("../../assets/images/vidas.png")}
        style={{
          width: 40,
          height: 40,
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        {personagem && <Text style={styles.textImage}>{personagem.healthPoints}</Text>}
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
