import React, { useRef, useEffect, useState } from "react";
import { View, StyleSheet, Text, Image, ImageBackground } from "react-native";
import { Alternativa } from "../index";
import { Button } from "react-native-elements";
import { cosmeticos } from "../../constants";
export default function Pergunta({
  cabeca = "tint1_head",
  pescoco = "tint1_neck",
  sapato = "blackShoe1",
  calca = "pantsBrown_long",
  hair = "blondeMan1",
  face = "face1",
  torax = "redShirt1",
  maos = "tint1_hand",
  bracos = "redArm_long",
  shirts,
}) {
  const urlHair = cosmeticos[hair + ".png"];
  console.log(urlHair);

  return (
    <View style={styles.container}>
      <View style={{ alignSelf: "center" }}>
        <ImageBackground
          source={cosmeticos[cabeca + ".png"]}
          style={{ height: 70, width: 60 }}
        >
          <Image
            style={{
              width: 60,
              height: 50,
              top: -10,
              backgroundColor: "transparent",
            }}
            resizeMode="contain"
            source={urlHair}
          />
          <Image
            style={{ width: 60, height: 50, top: -15, marginTop: -20 }}
            resizeMode="contain"
            source={cosmeticos[face + ".png"]}
          />
        </ImageBackground>
      </View>
      <View style={{ alignSelf: "center" }}>
        <Image
          style={{ height: 30, marginTop: -10, width: 30 }}
          resizeMode="contain"
          source={cosmeticos[pescoco + ".png"]}
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
          source={cosmeticos[bracos + ".png"]}
        />
        <Image
          style={{ height: 50, marginTop: -16, width: 50 }}
          resizeMode="contain"
          source={cosmeticos[torax + ".png"]}
        />
        <Image
          style={{ height: 50, marginTop: -20, width: 50, marginLeft: -10 }}
          resizeMode="contain"
          source={cosmeticos[bracos + ".png"]}
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
          source={cosmeticos[maos + ".png"]}
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
          source={cosmeticos[maos + ".png"]}
        />
      </View>
      <View style={{ alignSelf: "center" }}>
        <Image
          style={{ height: 45, marginTop: -20, width: 45 }}
          resizeMode="contain"
          source={cosmeticos["pantsBrown1.png"]}
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
          source={cosmeticos[calca + ".png"]}
        />
        <Image
          style={{ height: 45, marginTop: -20, width: 60, marginRight: -30 }}
          resizeMode="contain"
          source={cosmeticos[calca + ".png"]}
        />
      </View>
      <View
        style={{ alignSelf: "center", flexDirection: "row", marginRight: 0 }}
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
          source={cosmeticos[sapato + ".png"]}
        />
        <Image
          style={{ height: 45, marginTop: -20, width: 30, marginLeft: 40 }}
          resizeMode="contain"
          source={cosmeticos[sapato + ".png"]}
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
