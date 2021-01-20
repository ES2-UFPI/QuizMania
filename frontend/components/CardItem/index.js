import React from "react";

import { Image, View, Text, AsyncStorage } from "react-native";

import { Card, Button } from "react-native-elements";
import API from '../../services'
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
          {index * 2}
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
        onPress={async () => {
          // alert("Obtendo item..." + index)
          try {
            const response = await API.gastarGold({"expenseRequested": index*2})
            if(response.resultMessage == "Authorized") {
              let data = JSON.parse(await AsyncStorage.getItem("data"))
              if (data) {
                const {goldLocal, vidaLocal} = data
                // data.goldLocal -= index * 2
                if(data['vidaLocal'] === undefined || data['vidaLocal'] === null)
                data['vidaLocal'] = index 
                else
                data['vidaLocal'] += index
                await AsyncStorage.setItem("data", JSON.stringify(data))
              } else {
                data = {}
                data['goldLocal'] = index * 2
                data['vidaLocal'] = index
                await AsyncStorage.setItem("data", JSON.stringify(data))
              }
              //console.log("Oh", data)
              alert("Compra efetuada com sucesso!")
              } else {
                alert("Saldo inscuficiente!")
              }
          } catch(error) {
            //console.log(error)
            alert("Ocorreu um erro na compra...")
          }
        }}
      />
    </Card>
  );
}
