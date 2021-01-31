import axios from 'axios'


class ApiConnection {
  constructor() {
    this.api = axios.create({
      baseURL: "https://quizmaniaapplication.azurewebsites.net/"
    })
  }

  async obterPersonagem({ id=1 }) {
    try {
      const response = await this.api.get(`character/${id}`);
      return response.data;
    } catch (error) {
      alert(error.toString()) 
    }
  }


  async obterQuizzes() {
    try {
      const response = await this.api.get(`quiz`);
      return response.data;
    } catch (error) {
      alert(error.toString()) 
    }
  }

  async detalharQuiz(id) {
    try {
      const response = await this.api.get(`quiz/${id}`);
      return response.data;
    } catch (error) {
      alert(error.toString()) 
    }
  }

  async responderQuiz(data) {
    data["characterId"] = 1
    try {
      //console.log(data)
      const response = await this.api.post(`quiz/feedback`, data);
      return response.data;
    } catch (error) {
      //alert(error)
      //console.log("error: ", error.response) 
    }
  }

  async gastarGold (data) {
    data["characterId"] = 1
    try {
      const response = await this.api.post(`/character/expendGold`, data);
      
      return response.data;
    } catch (error) {
      throw error
    }
  }

  async deletarQuiz(quizId) {
    const data = {quizId}
    data["characterId"] = 1
    try {
      const response = await this.api.delete(`quiz`, {data});
      
      return response.data;
    } catch (error) {
      throw error
    }

  }

  async deletarPergunta(data) {
    //console.log(data)
    try {
      const response = await this.api.delete(`quiz/question`, {data});
      
      return response.data;
    } catch (error) {
      throw error
    }

  }
  async criarPergunta(data) {
    data["characterId"] = 1
    try {
      const response = await this.api.post(`quiz/question`, data);
      
      return response.data;
    } catch (error) {
      throw error
    }

  }

  async criarQuiz(data) {
    data["ownerId"] = 1
    try {
      const response = await this.api.post(`quiz/`, data);
      
      return response.data;
    } catch (error) {
      throw error
    }
  }

  async equiparPersonagem(item) {
    data = {}
    data['itemName'] = item
    data["characterId"] = 1
    try {
      const response = await this.api.patch(`character/items`, data);
      return response.data;
    } catch (error) {
      throw error
    }
  }

  async recuperarItensComprados() {
    id = 1
    try {
      const response = await this.api.get(`character/items/${id}`);
      return response.data;
    } catch (error) {
      throw error
    }
  }

  async comprarItem(item) {
    data = {}
    data['itemName'] = item
    data["characterId"] = 1
    data['quantity'] = 1
    try {
      const response = await this.api.post(`/character/items/purchase`, data);
      
      return response.data;
    } catch (error) {
      throw error
    }
  }

  async recuperarItens() {
    try {
      const response = await this.api.get(`/character/items`)
      return response.data
    } catch (error) {
      throw error
    }
  }

  async recuperarGuildas() {
    try {
      const response = await this.api.get(`/character/guilds`)
      return response.data
    } catch (error) {
      throw error
    }
  }

  async participarGuilda(id) {
    data = {
      characterId: 1,
      guildId: id
    }
    try {
      const response = await this.api.patch(`/character/guilds`, data)
      return response.data
    } catch (error) {
      throw error
    }
  }
}

export default new ApiConnection();