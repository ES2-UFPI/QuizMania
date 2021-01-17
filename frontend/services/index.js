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
      console.log(data)
      const response = await this.api.post(`quiz/feedback/`, data);
      return response.data;
    } catch (error) {
      //alert(error)
      console.log("error: ", error.response) 
    }
  }

  async gastarGold (data) {
    data["characterId"] = 1
    try {
      const response = await this.api.post(`expendGold/`, data);
      
      return response.data;
    } catch (error) {
      throw error
    }
  }

  async deletarQuiz(data) {
    data["characterId"] = 1
    try {
      const response = await this.api.delete(`quiz/`, data);
      
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
}

export default new ApiConnection();