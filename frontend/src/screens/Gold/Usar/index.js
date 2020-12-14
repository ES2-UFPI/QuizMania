import React from "react";
import { ScrollView, View, Text, FlatList, Image } from "react-native";
import { Container, Header, ResponsiveList } from "../../../../components";
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
              <Card containerStyle={{ width: 130, }}>
                <Image
                  source={require("../../../../assets/images/vidas.png")}
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
                    source={require("../../../../assets/images/moeda.png")}
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
            ))}
        </ResponsiveList>
      </ScrollView>
    </Container>
  );
}
