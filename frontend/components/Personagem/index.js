import React, { useRef, useEffect, useState } from "react";
import {
  View,
  StyleSheet,
  Text,
  Image,
  ImageBackground,
  AsyncStorage,
} from "react-native";
import { Alternativa } from "../index";
import { Button } from "react-native-elements";
import { cosmeticos } from "../../constants";
import API from '../../services'
export default function Pergunta({ stepped, navigation }) {
  const [cabeca, setcabeca] = useState("tint1_head");
  const [pescoco, setpescoco] = useState("tint1_neck");
  const [sapato, setsapato] = useState("blackShoe1");
  const [calca, setcalca] = useState("pantsBrown_long");
  const [cabelo, setcabelo] = useState("blondMan1");
  const [rosto, setrosto] = useState("face1");
  const [torax, settorax] = useState("redShirt1");
  const [maos, setmaos] = useState("tint1_hand");
  const [bracos, setbracos] = useState("redArm_long");
  const urlHair = cosmeticos[cabelo + ".png"].image;
  const [step, setStep] = useState(0);

  // useEffect(() => {
  //   async function getData() {
  //     const data = await AsyncStorage.getItem("data");
  //     if (data) {
  //       const response = JSON.parse(data);
  //       //console.log(response);
  //       const {
  //         head,
  //         neck,
  //         shoe,
  //         hair,
  //         pants,
  //         arm,
  //         hand,
  //         shirt,
  //         face,
  //       } = response;
  //       setcabeca(head ? head : "tint1_head");
  //       setpescoco(neck ? neck : "tint1_neck");
  //       setsapato(shoe ? shoe : "blackShoe1");
  //       setcalca(pants ? pants : "pantsBrown_long");
  //       setcabelo(hair ? hair : "blondeMan1");
  //       setrosto(face ? face : "face1");
  //       settorax(shirt ? shirt : "redShirt1");
  //       setmaos(hand ? hand : "tint1_hand");
  //       setbracos(arm ? arm : "redArm_long");
  //       //console.log("aqui é a face", face);
  //       //console.log("aqui n é a face", rosto);
  //       setStep(step + 1);
  //     }
  //   }
  //   getData();
  // }, [stepped]);
  useEffect(() => {
    async function atualizarPersonagem() {
      try {
        const vincularTipo = {
            Head: setcabeca,
            Neck: setpescoco,
            Shoe: setsapato,
            Pants: setcalca,
            Hair: setcabelo,
            Face: setrosto,
            Shirt: settorax,
            Hand: setmaos,
            Arm: setbracos,
        }
        const response = await API.recuperarItensComprados()
        //console.log(response)
        let equipados = response.items.filter((item, index) => item.isEquipped)
        equipados.map((item, index) => {
          //console.log(item)
          item.item.slotType != "Other" ? vincularTipo[item.item.slotType](item.item.name.replace(".png", "")) : undefined
        })
  
      } catch (error) {
        alert("Não foi possível atualizar os itens do seu personagem...")
        //console.log(error)
      }
    }
    atualizarPersonagem()
    const unsubscribe = navigation.addListener("focus", () => {
      async function getData() {
        const data = await AsyncStorage.getItem("data");
        if (data) {
          const response = JSON.parse(data);
          //console.log(response);
          const {
            head,
            neck,
            shoe,
            hair,
            pants,
            arm,
            hand,
            shirt,
            face,
          } = response;
          setcabeca(head ? head : "tint1_head");
          setpescoco(neck ? neck : "tint1_neck");
          setsapato(shoe ? shoe : "blackShoe1");
          setcalca(pants ? pants : "pantsBrown_long");
          setcabelo(hair ? hair : "blondMan1");
          setrosto(face ? face : "face1");
          settorax(shirt ? shirt : "redShirt1");
          setmaos(hand ? hand : "tint1_hand");
          setbracos(arm ? arm : "redArm_long");
          //console.log("aqui é a face", face);
          //console.log("aqui n é a face", rosto);
          setStep(step + 1);
        }
        atualizarPersonagem()
      }

      atualizarPersonagem();
      setStep(step + 1);
      //console.log("chamou no person");
    });

    // Return the function to unsubscribe from the event so it gets removed on unmount
    return unsubscribe;
  }, [navigation]);

  return (
    <View style={styles.container}>
      <View style={{ alignSelf: "center" }}>
        <ImageBackground
          source={cosmeticos[cabeca + ".png"].image}
          resizeMode="contain"
          style={{ height: 70, width: 60, marginBottom: -5 }}
        >
          <Image
            style={{
              width: 60,
              height: 50,
              top: -10,
              backgroundColor: "transparent",
            }}
            resizeMode="center"
            source={urlHair}
          />
          <Image
            style={{
              width: 40,
              height: 50,
              alignSelf: "center",
              top: -15,
              marginTop: -20,
              paddingBottom: 3,
            }}
            resizeMode="center"
            source={cosmeticos[rosto + ".png"].image}
          />
        </ImageBackground>
      </View>
      <View style={{ alignSelf: "center" }}>
        <Image
          style={{ height: 30, marginTop: -10, width: 30 }}
          resizeMode="contain"
          source={cosmeticos[pescoco + ".png"].image}
        />
      </View>
      <View
        style={{ alignSelf: "center", flexDirection: "row", marginTop: -2 }}
      >
        <Image
          style={{
            height: 50,
            marginTop: -20,
            width: 50,
            marginRight: -10,
            transform: [{ rotateY: "180deg" }],
          }}
          resizeMode="contain"
          source={cosmeticos[bracos + ".png"].image}
        />
        <Image
          style={{ height: 50, marginTop: -16, width: 50 }}
          resizeMode="contain"
          source={cosmeticos[torax + ".png"].image}
        />
        <Image
          style={{ height: 50, marginTop: -20, width: 50, marginLeft: -10 }}
          resizeMode="contain"
          source={cosmeticos[bracos + ".png"].image}
        />
      </View>
      <View style={{ alignSelf: "center", flexDirection: "row" }}>
        <Image
          style={{
            height: 20,
            zIndex: -1,
            marginTop: -20,
            marginRight: 50,
            width: 20,
          }}
          resizeMode="contain"
          source={cosmeticos[maos + ".png"].image}
        />
        <Image
          style={{
            height: 20,
            zIndex: -1,
            marginTop: -20,
            marginLeft: 50,
            width: 20,
          }}
          resizeMode="contain"
          source={cosmeticos[maos + ".png"].image}
        />
      </View>
      <View style={{ alignSelf: "center" }}>
        <Image
          style={{ height: 45, marginTop: -20, width: 45 }}
          resizeMode="contain"
          source={cosmeticos["pantsBrown1.png"].image}
        />
      </View>

      <View
        style={{ alignSelf: "center", flexDirection: "row", marginRight: 30 }}
      >
        <Image
          style={{
            height: 45,
            marginTop: -20,
            width: 60,
            marginRight: -30,
            transform: [{ rotateY: "180deg" }],
          }}
          resizeMode="contain"
          source={cosmeticos[calca + ".png"].image}
        />
        <Image
          style={{ height: 45, marginTop: -20, width: 60, marginRight: -30 }}
          resizeMode="contain"
          source={cosmeticos[calca + ".png"].image}
        />
      </View>
      <View
        style={{
          alignSelf: "center",
          flexDirection: "row",
          marginRight: 0,
          justifyContent: "space-between",
        }}
      >
        <Image
          style={{
            height: 45,
            marginTop: -20,
            width: 30,
            marginRight: -30,
            transform: [{ rotateY: "180deg" }],
          }}
          resizeMode="contain"
          source={cosmeticos[sapato + ".png"].image}
        />
        <Image
          style={{ height: 45, marginTop: -20, width: 30, marginLeft: 50 }}
          resizeMode="contain"
          source={cosmeticos[sapato + ".png"].image}
        />
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    justifyContent: "space-around",
    flex: 0.7,
  },
  containerRespostas: {
    justifyContent: "space-between",
    marginVertical: 10,
  },

  titulo: {
    fontSize: 21,
    fontWeight: "bold",
  },
});
