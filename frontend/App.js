import { StatusBar } from "expo-status-bar";
import React from "react";
import { StyleSheet, Text, View } from "react-native";
import {
  ResponderQuiz,
  ListarQuiz,
  UsarGold,
  ProgressaoNivel,
  XpGanho,
  GoldGanho,
  CriarQuiz,
  Perfil,
} from "./src/screens";

import { MaterialCommunityIcons } from "@expo/vector-icons";
import { NavigationContainer } from "@react-navigation/native";
import { createStackNavigator } from "@react-navigation/stack";
import { createMaterialBottomTabNavigator } from "@react-navigation/material-bottom-tabs";

const Tab = createMaterialBottomTabNavigator();

const Stack = createStackNavigator();
const QuizStack = () => (
  <Stack.Navigator>
    <Stack.Screen name="Criar Quiz" component={CriarQuiz} />
    <Stack.Screen name="Listar Quizzes" component={ListarQuiz} />
    <Stack.Screen name="Responder Quiz" component={ResponderQuiz} />
    <Stack.Screen name="Progressão" component={ProgressaoNivel} />
    <Stack.Screen name="Gold Ganho" component={GoldGanho} />
    <Stack.Screen name="XP Ganho" component={XpGanho} />
  </Stack.Navigator>
);

export default function App() {
  return (
    <NavigationContainer>
      <Tab.Navigator>
        {/* <Tab.Screen
          name="Perfil"
          component={Perfil}
          options={{
            tabBarIcon: ({ color }) => (
              <MaterialCommunityIcons
                name="account-question-outline"
                color={color}
                size={25}
              />
            ),
          }}
        /> */}
        <Tab.Screen
          name="Quizzes"
          component={QuizStack}
          options={{
            tabBarIcon: ({ color }) => (
              <MaterialCommunityIcons
                name="frequently-asked-questions"
                color={color}
                size={26}
              />
            ),
          }}
        />
        <Tab.Screen
          name="Loja"
          component={UsarGold}
          options={{
            tabBarIcon: ({ color }) => (
              <MaterialCommunityIcons
                name="cart"
                color={color}
                size={25}
              />
            ),
          }}
        />
      </Tab.Navigator>
    </NavigationContainer>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    alignItems: "center",
    justifyContent: "center",
  },
});
