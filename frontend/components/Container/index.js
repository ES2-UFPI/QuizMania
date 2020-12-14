import React from "react";
import { View, ImageBackground, StyleSheet } from "react-native";
import { BACKGROUND_COLOR } from "../../constants";
import { getStatusBarHeight } from "react-native-status-bar-height";

export default function container({ children }) {
  return (
    <ImageBackground source={require("../../assets/images/background.png")}style={styles.image} imageStyle={{opacity: 0.15}}>
      <View style={styles.container}>
      {children}
      </View>
    </ImageBackground>
  );
}

const styles = StyleSheet.create({
  image: {
    width: '100%',
    height: '100%',
    opacity: 1
  },
  container: {
    backgroundColor: 'transparent',
    flex: 1,
    marginTop: getStatusBarHeight() + 10,
    marginHorizontal: 10,
  },
});
