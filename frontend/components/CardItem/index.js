import React from "react";

import { Image, View, Text } from "react-native";

import { Card, Button } from "react-native-elements";

export default function CardItem({index}) {
  return (
    <Card containerStyle={{ width: 130 }}>
      <Image
        source={require("../../assets/images/vidas.png")}
        style={{ height: 40, width: 40, alignSelf: "center" }}
      />
      <Card.Title>Pack {index}* vidas</Card.Title>
      <Card.Divider />

      <View
        style={{
          flexDirection: "row",
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <Text
          style={{
            fontStyle: "italic",
            color: "gray",
            margin: 0,
            fontWeight: "bold",
            marginRight: 5,
            marginBottom: 5,
          }}
        >
          {index * 600}
        </Text>
        <Image
          source={require("../../assets/images/moeda.png")}
          style={{ height: 15, width: 15, marginBottom: 5 }}
        />
      </View>
      <Card.Divider />
      <Button
        title="Obter"
        type="clear"
        onPress={() => alert("Obtendo item..." + index)}
      />
    </Card>
  );
}
