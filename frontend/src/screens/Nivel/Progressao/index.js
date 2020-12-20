import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity } from "react-native";
import { Button, Card } from "react-native-elements";
import { Container, Header, ProgressBar } from "../../../../components";

export default function AvancoNivel({ navigation }) {
  const [xpAtual, setXpAtual] = useState(300);

  function isNextLevel() {
    return xpAtual + 300 >= 600;
  }

  return (
    <Container>
      <Header />
      <View>
        <Text style={{ marginBottom: 20 }}>
          Progress with animation and increased height
        </Text>
        <ProgressBar progress={(xpAtual/300) * 100} height={25} backgroundColor="#4a0072" />
      </View>
    </Container>
  );
}
