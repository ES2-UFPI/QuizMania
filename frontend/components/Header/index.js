import React from "react";
import { View, StyleSheet, Image, ImageBackground, Text } from "react-native";
import { BACKGROUND_COLOR } from "../../constants";

export default function Header() {
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
        <Text style={styles.textImage}>600</Text>
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
        <Text style={styles.textImage}>4</Text>
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
