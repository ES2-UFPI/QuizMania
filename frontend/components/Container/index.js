import React, { useEffect, useState } from "react";
import { ScrollView, ImageBackground, StyleSheet, View, RefreshControl } from "react-native";
import { BACKGROUND_COLOR } from "../../constants";
import { Header } from "../index";
import { getStatusBarHeight } from "react-native-status-bar-height";

export default function container({ children, notscroll, navigation,refresh, flex }) {
  const [render, setRender] = useState(true);
  const [step, setStep] = useState(0)
  const [refreshing, setRefreshing] = useState(false)
  useEffect(() => {
    // const unsubscribe = navigation.addListener("focus", () => {
    //   setStep(step + 1);
    //   ////console.log("chamou")
    // });

    // // Return the function to unsubscribe from the event so it gets removed on unmount
    // return unsubscribe;
    // alert("oi")
  }, []);

  function onRefresh() {
    setStep(step +1)
  }
  if (notscroll)
    return (
      <ImageBackground
        source={require("../../assets/images/background.png")}
        style={styles.image}
        imageStyle={{ opacity: 0.15 }}
      >
        <View style={styles.container}>
          <Header navigation={navigation} />
          <React.Fragment>{children}</React.Fragment>
        </View>
      </ImageBackground>
    );
  return (
    <ImageBackground
      source={require("../../assets/images/background.png")}
      style={styles.image}
      imageStyle={{ opacity: 0.15 }}
    >
      <ScrollView
        style={styles.container}
        refreshControl={
          refresh ? <RefreshControl refreshing={refreshing} onRefresh={onRefresh} /> : undefined
        }
      >
        <Header navigation={navigation} />
        <React.Fragment>{children}</React.Fragment>
      </ScrollView>
    </ImageBackground>
  );
}

const styles = StyleSheet.create({
  image: {
    width: "100%",
    height: "100%",
    opacity: 1,
  },
  container: {
    backgroundColor: "transparent",
    flex: 1,
    marginTop: getStatusBarHeight() + 10,
    marginHorizontal: 10,
    display: 'flex'
  },
});
