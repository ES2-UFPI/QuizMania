import React, { useState, useEffect } from "react";
import { Text, View, FlatList, TouchableOpacity } from "react-native";
import { Button, Card } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito } from "../../../components";
import API from '../../../services'
export default function ListarQuizzes({navigation}) {
  const [quizzes, setQuizzes] = useState([]);



  
  const numColumns = 10;
  return (
    <Container navigation={navigation} >
      
    </Container>
  );
}
