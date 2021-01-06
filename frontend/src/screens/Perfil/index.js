import React, { useState, useEffect } from "react";
import Expo from "expo";
import { GLView } from 'expo-gl'

import * as THREE from "three";
import ExpoTHREE from "expo-three";
import { Text, View, FlatList, TouchableOpacity } from "react-native";
import { Button, Card } from "react-native-elements";
import { Container, Header, Pergunta, Gabarito } from "../../../components";
import API from '../../../services'
export default function ListarQuizzes({navigation}) {
  const [quizzes, setQuizzes] = useState([]);



  const onGLContextCreate = async gl => {
    const scene = new THREE.Scene();
    const camera = new THREE.PerspectiveCamera(
      75, gl.drawingBufferWidth / gl.drawingBufferHeight, 0.1, 1000
    );
    const renderer = ExpoTHREE.createRenderer({ gl });
    renderer.setSize(gl.drawingBufferWidth, gl.drawingBufferHeight);
  
    const geometry = new THREE.SphereBufferGeometry(1, 36, 36);
    const material = new THREE.MeshBasicMaterial({
      map: await ExpoTHREE.createTextureAsync({
        asset: Expo.Asset.fromModule(require("../../../assets/personagem/body.tex.png"))
      })
    });
    const sphere = new THREE.Mesh(geometry, material);    
    scene.add(sphere);
    camera.position.z = 2;
    const render = () => {
      requestAnimationFrame(render);
      sphere.rotation.x += 0.01;
      sphere.rotation.y += 0.01;
      renderer.render(scene, camera);
      gl.endFrameEXP();
    };
    render();
  };
  const numColumns = 10;
  return (
    <Container navigation={navigation} >
      
    </Container>
  );
}
