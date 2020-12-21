import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity } from "react-native";
import { Button, Card } from "react-native-elements";
import { Container, Header, ProgressBar } from "../../../../components";
import { useFocusEffect } from '@react-navigation/native';

export default function AvancoNivel({ navigation }) {
  const [xpAtual, setXpAtual] = useState(300);
  const [rendernew, setRendernew] = useState(rendernew)
  const [xpMax, setxpMax] = useState(600)
  function isNextLevel() {
    return xpAtual + 300 >= xpMax;
  }

  useFocusEffect(
    React.useCallback(() => {
      const unsubscribe = () => {
        setxpMax(600)
      };

      return () => unsubscribe();
    }, [])
  );

  function completeProgress() {
    if(isNextLevel()) {
      setxpMax(900)
    }
  }

  return (
    <Container>
      <Header />
      <View>
        <Text style={{ marginBottom: 20 }}>
          Você ganhou -- XP
        </Text>
        <ProgressBar onCompletion={completeProgress.bind(this)} min={xpMax-300} max={xpMax} progress={((xpAtual+300)/xpMax) * 100} height={25} backgroundColor="#4a0072" />
        <Button title="Continuar" onPress={() => 
          navigation.navigate("Responder Quiz", {notReload: true})
         }/>
      </View>
    </Container>
  );
}
