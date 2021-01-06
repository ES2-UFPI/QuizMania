import React, { useState, useEffect } from "react";
import { Text, View, FlatList, Modal, StyleSheet, Alert, Image, TouchableOpacity } from "react-native";
import { Button, Card } from "react-native-elements";
import { Container, Header, ProgressBar } from "../../../../components";
import { useFocusEffect } from "@react-navigation/native";

export default function XpGanho({ navigation, route }) {
  const [xpAtual, setXpAtual] = useState(300);
  const [xpMax, setxpMax] = useState(600);
  function isNextLevel() {
    return xpAtual + 300 >= xpMax;
  }


  function completeProgress() {
    if (isNextLevel()) {
      setxpMax(900);
    }
  }

  const [rendernew, setRendernew] = useState(false);
  useEffect(() => {
    setTimeout(() => {
      setRendernew(true);
    }, 2000);
  }, []);

  return (
    <Container navigation={navigation} >
      {/* <View style={{justifyContent: 'space-between', flex: 1, height: 500, }}>
        <Text style={{ marginBottom: 20 }}>Você ganhou -- XP</Text>
        <ProgressBar
          onCompletion={completeProgress.bind(this)}
          min={xpMax - 300}
          max={xpMax}
          progress={((xpAtual + 300) / xpMax) * 100}
          height={25}
          backgroundColor="#4a0072"
        />
        <Button
          title="Continuar"
          onPress={() => {
            if(true) {
              navigation.navigate("Gold Ganho")
            } else {
              navigation.goBack();
              
              navigation.goBack();
            }
          }}
        />
      </View> */}
      <View style={styles.centeredView}>
        <Modal
          animationType="fade"
          transparent
          visible={rendernew}

          style={{
            backgroundColor: "rgba(0,0,0,0.3)",
            alignItems: "center",
            justifyContent: "center",
            flex: 1,
          }}
          onRequestClose={() => {
            Alert.alert("Modal has been closed.");
          }}
        >
          <View style={styles.centeredView}>
            <View style={styles.modalView}>
              <Image
                style={{
                  backgroundColor: "transparent",
                  position: "relative",
                  alignSelf: 'center',
                  width: 200,
                  height: 300,
                }}
                autoPlay
                loop
                source={require("../../../../assets/images/success.gif")}
              />
              <Text style={{marginVertical: 30}}>Você ganhou {route.params.dados.xpGanho} XP</Text>
              <Button title="Continuar " onPress={() => {
                navigation.navigate("Gold Ganho", {dados: route.params.dados})
                setRendernew(false)
              }}/>
            </View>
          </View>
        </Modal>
      </View>
    </Container>
  );
}
const styles = StyleSheet.create({
  centeredView: {
    flex: 1,
    zIndex: -10,
    position: 'absolute',
    width: '100%',
    bottom: 0,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: 'rgba(0,0,0,0.5)',
  },
  modalView: {
    backgroundColor: "white",
    borderRadius: 20,
    padding: 35,
    alignItems: "center",
    justifyContent:'space-between',
    alignSelf: 'center',
    shadowColor: "#000",

    marginVertical: 100,
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    position:'relative',
    elevation: 5,
  },
  openButton: {
    backgroundColor: "#F194FF",
    borderRadius: 20,
    padding: 10,
    elevation: 2,
  },
  textStyle: {
    color: "white",
    fontWeight: "bold",
    textAlign: "center",
  },
  modalText: {
    marginBottom: 15,
    textAlign: "center",
  },
});
