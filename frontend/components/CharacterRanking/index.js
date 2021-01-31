import React from 'react'
import { ScrollView, StyleSheet, View, Text} from 'react-native'


export default function ResponsiveList({porcentagem, nome, xp}) {
  console.log(porcentagem.toString() + '%')
  return(
    <View style={{...styles.container, height: '100%'}}>
      <Text style={{textAlign: 'center'}}>{nome}</Text>
      <View style={{backgroundColor: 'black', height: (porcentagem < 90 ? porcentagem: 90).toString() + '%', width: '100%'}}></View>
      <Text style={{textAlign: 'center'}}>{xp}XP</Text>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    width: 55,
    marginHorizontal: 10,
    flexDirection: 'column-reverse',
    alignContent: 'flex-end',
  },
})