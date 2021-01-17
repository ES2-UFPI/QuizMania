import React from "react";
import { ScrollView, View, Text, FlatList, Image } from "react-native";
import { Container, Header, ResponsiveList, CardItem } from "../../../../components";
import { Card, Button } from "react-native-elements";

export default function Usar({navigation}) {
  return (
    <Container navigation={navigation} >
      <Text style={{ fontSize: 30, fontWeight: "bold" }}>Loja</Text>
      <View style={{ justifyContent: "center" }}></View>
      <ScrollView contentContainerStyle={{ paddingVertical: 20, }}>
        <ResponsiveList>
          {Array(11)
            .fill(0)
            .map((item, index) => (
              index != 0 ? <CardItem index={index}/> : undefined
            ))}
        </ResponsiveList>
      </ScrollView>
    </Container>
  );
}
