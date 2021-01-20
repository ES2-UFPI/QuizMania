import React, { useState, useEffect } from "react";
import {
  ScrollView,
  View,
  Text,
  FlatList,
  Image,
  Picker,
  AsyncStorage,
} from "react-native";
import {
  Container,
  Header,
  ResponsiveList,
  CardItem,
} from "../../../../components";
import { Card, Button } from "react-native-elements";
import { cosmeticos } from "../../../../constants";
import API from "../../../../services";

function Usar({ navigation,route }) {
  const [itens, setItens] = useState([]);
  const [itensFiltrados, setItensFiltrados] = useState(
    []
  );
  const [categoria, setCategoria] = useState("----");
  const [setp, setStep] = useState(0);

  const categorias = [
    "Other",
    "Arm",
    "Hand",
    "Head",
    "Hair",
    "Neck",
    "Shoe",
    "Pants",
    "Face",
    "Shirt",
  ];

  const force = setStep.bind(this)

  async function getData() {
    try {
      const response = await API.recuperarItens()
      const itensComprados = await API.recuperarItensComprados()
      const {items} = itensComprados
      setItens(response.map((item, index) => {
        const is_comprado = items.find((element) => element.item.name == item.name)
        let is_equipado;
        if(is_comprado)
          is_equipado = is_comprado.isEquipped
        const new_value = {...item, is_comprado, is_equipado}
        return new_value
      }))
      setItensFiltrados(itens.filter((item, index) => item.slotType == categoria))
    } catch (error) {
      alert("Ocorreu um erro ao obter os itens da loja...")
      console.log(error)
    }
  }


  useEffect(() => {
    getData()
    force(setp + 1);
  }, [navigation])


  function filtrar(value) {
    console.log(itens.filter((item, index) => item.slotType == value))
    setCategoria(value);
    if (value === "") {
      setItensFiltrados(itens);
    }
    setItensFiltrados(itens.filter((item, index) => item.slotType == value));
  }

  return (
    <React.Fragment>
      <View style={{ justifyContent: "center" }}>
        <Picker
          onValueChange={(value) => filtrar(value)}
          selectedValue={categoria}
        >
          <Picker.Item
              label={"Selecione uma categoria"}
              value={"----"}
            />
          {categorias.map((item, index) => (
            <Picker.Item
              label={item[0].toUpperCase() + item.substr(1)}
              value={item}
            />
          ))}
        </Picker>
      </View>
      <FlatList
        data={itensFiltrados}
        numColumns={2}
        renderItem={({ item, index }) => (
          <Card containerStyle={{ width: 150 }}>
            {/* {console.log(item)} */}
           {cosmeticos[item.name] && <Image
              source={cosmeticos[item.name].image || {}}
              resizeMode="contain"
              style={{ height: 40, width: 40, alignSelf: "center" }}
            />}
            <Card.Title>{item.name.replace(".png", "")}</Card.Title>
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
                {item.cost}
              </Text>
              <Image
                source={require("../../../../assets/images/moeda.png")}
                style={{ height: 15, width: 15, marginBottom: 5 }}
              />
            </View>
            <Card.Divider />
            {item.is_equipado ?
              <Button
              title="Equipado"
              type="clear"
              onPress={() => {
                force(setp + 1)
              }}
            />
            :
            item.is_comprado?
            <Button
              title="Equipar"
              type="clear"
              onPress={async () => {
                try {
                  const response = await API.equiparPersonagem(item.name)
                  alert("Personagem equipado com sucesso!!")
                  await getData()
                  force(setp + 1)
                  await getData()
                  force(setp + 1)
                } catch (error) {
                  alert("Não foi possível equipar seu personagem com esse item.")
                  console.log(error)
                }
              }}
            /> 
            : 
            <Button
              title="Obter"
              type="clear"
              onPress={async () => {
                try {
                  const response = await API.comprarItem(item.name)
                  await getData()
                  force(setp + 1)
                  alert("Item comprado com sucesso!") 
                } catch (error) {
                  alert("O item não pode ser comprado...")
                }
              }}
            />
          }
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
    </React.Fragment>
  );
}

function Vidas({ navigation }) {
  return (
    <ScrollView contentContainerStyle={{ paddingVertical: 20 }}>
      <ResponsiveList>
        {Array(11)
          .fill(0)
          .map((item, index) =>
            index != 0 ? <CardItem index={index} /> : undefined
          )}
      </ResponsiveList>
    </ScrollView>
  );
}

import { createMaterialTopTabNavigator } from "@react-navigation/material-top-tabs";

const Tab = createMaterialTopTabNavigator();

export default function MyTabs({ navigation }) {
  const [step, setStep] = useState(0)

  function reRender() {
    setStep(step + 1)
  }

  return (
    <Container navigation={navigation} notscroll>
      <Text style={{ fontSize: 30, fontWeight: "bold" }}>Loja</Text>
      <View style={{ justifyContent: "center" }}></View>
      <Tab.Navigator style={{backgroundColor: 'transparent'}}>
        <Tab.Screen name="Itens" component={Usar} initialParams={{navigation, reRender: reRender.bind(this)}}/>
        <Tab.Screen name="Vidas" component={Vidas} initialParams={{navigation, reRender: reRender.bind(this)}}/>
      </Tab.Navigator>
    </Container>
  );
}
