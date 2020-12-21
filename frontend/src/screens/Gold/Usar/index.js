import React from "react";
import { ScrollView, View, Text, FlatList, Image } from "react-native";
import { Container, Header, ResponsiveList, CardItem } from "../../../../components";
import { Card, Button } from "react-native-elements";

export default function Usar() {
  return (
    <Container>
      <Header />
      <Text style={{ fontSize: 30, fontWeight: "bold" }}>Loja</Text>
      <View style={{ justifyContent: "center" }}></View>
      <ScrollView contentContainerStyle={{ paddingVertical: 20 }}>
        <ResponsiveList>
          {Array(10)
            .fill(0)
            .map((item, index) => (
              <CardItem index={index}/>
            ))}
        </ResponsiveList>
      </ScrollView>
    </Container>
  );
}
