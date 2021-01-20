import React, {useState} from "react";
import { ScrollView, View, Text, FlatList, Image, Picker, AsyncStorage } from "react-native";
import {
  Container,
  Header,
  ResponsiveList,
  CardItem,
} from "../../../../components";
import { Card, Button } from "react-native-elements";
import { cosmeticos } from "../../../../constants";
import API from '../../../../services'

export default function Usar({ navigation }) {
  const [itens, setItens] = useState(Object.entries(cosmeticos))
  const [itensFiltrados, setItensFiltrados] = useState(itens.filter((item, index) => item[1].tipo == 'arm'))
  const [categoria, setCategoria] = useState("arm")
  const [setp, setStep] = useState(0)

  const categorias = ["arm","hand", "head","hair" , 'neck', 'shoe', 'pants', 'face', 'shirt']
  function filtrar(value) {
    setCategoria(value)
    if(value === "") {
      setItensFiltrados(itens)
    }
    setItensFiltrados(itens.filter((item, index) => item[1].tipo == value))

  }

  return (
    <Container navigation={navigation}>
      <Text style={{ fontSize: 30, fontWeight: "bold" }}>Loja</Text>
      <View style={{ justifyContent: "center" }}>
        <Picker
          onValueChange={(value) => filtrar(value)}
          selectedValue={categoria}
        >
          {categorias.map((item, index) => (
            <Picker.Item label={item[0].toUpperCase() + item.substr(1)} value={item}/>
          ))}
        </Picker>
      </View>
      <FlatList
        data={itensFiltrados}
        numColumns={2}
        renderItem={({ item, index }) => (
          <Card containerStyle={{ width: 150 }}>
            {/* {console.log(item)} */}
            <Image
              source={item[1].image}
              resizeMode='contain'
              style={{ height: 40, width: 40, alignSelf: "center" }}
            />
            <Card.Title>{item[0].replace(".png", "")}</Card.Title>
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
                source={require("../../../../assets/images/moeda.png")}
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
                  const response = {expenseAuthorized: true}
                  if (response.expenseAuthorized) {
                    let data = JSON.parse(await AsyncStorage.getItem("data"));
                    if (data) {
                      data.goldLocal -= index * 2;
                      data[item[1].tipo] = item[0].replace(".png", "");
                      await AsyncStorage.setItem("data", JSON.stringify(data));
                    } else {
                      data = {};
                      data[item[1].tipo] = item[0].replace(".png", "")
                      await AsyncStorage.setItem("data", JSON.stringify(data));
                    }
                    alert("Compra efetuada com sucesso!");
                  } else {
                    alert("Saldo inscuficiente!");
                  }
                } catch (error) {
                  console.log(error);
                  alert("Ocorreu um erro na compra...");
                }
              }}
            />
          </Card>
        )}
      />

      {/* <ScrollView contentContainerStyle={{ paddingVertical: 20, }}>
        <ResponsiveList>
          {Array(11)
            .fill(0)
            .map((item, index) => (
              index != 0 ? <CardItem index={index}/> : undefined
            ))}
        </ResponsiveList>
      </ScrollView> */}
    </Container>
  );
}
