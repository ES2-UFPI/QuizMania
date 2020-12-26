import React, { useState, useEffect } from "react";
import {
  Text,
  View,
  FlatList,
  TouchableOpacity,
  Modal,
  StyleSheet,
  Image,
  TouchableHighlight,
} from "react-native";
import { Button, Card } from "react-native-elements";
import { Container, Header, ProgressBar } from "../../../../components";
import { useFocusEffect } from "@react-navigation/native";
import LottieView from "lottie-react-native";

export default function AvancoNivel({ navigation }) {
  const [xpAtual, setXpAtual] = useState(300);
  const [rendernew, setRendernew] = useState(false);
  const [xpMax, setxpMax] = useState(600);
  function isNextLevel() {
    return xpAtual + 300 >= xpMax;
  }

  useEffect(() => {
    setTimeout(() => {
      setRendernew(true);
    }, 2000);
  }, []);

  function completeProgress() {
    if (isNextLevel()) {
      setxpMax(900);
    }
  }

  return (
    <Container notscroll>
      <Header />
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
              <Text style={{marginVertical: 30}}>Você avançou de nível!</Text>
              <Button title="Continuar "/>
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
