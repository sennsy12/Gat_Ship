import { defineStore } from 'pinia'
import axios from 'axios'
import router from '../router/index'




export const useAuthStore = defineStore('auth', {
    state: () => ({
      token: localStorage.getItem('token') || null,
      authUser: null,  //null kadhig
      user: null
      
    }),

    // For user name display 
    actions: {
        async submitHandler(credentials) {
        console.log("this credential",credentials)
        try {
          const response = await axios.post('login', {
            ...credentials
          });
          
          this.token = response.data.token
          localStorage.setItem('token', response.data.token);
          //this.authUser = response.data
          //console.log(response.data)
          return Promise.resolve();
      } catch (error) {
          console.error(error);
          return Promise.reject(error);
      }
      },
      logout() {
        console.log("logout")
        this.token = null
        localStorage.removeItem('token')
        this.user = null
        router.push("/")
        location.reload()
      },

      
    },
    
    getters:{
        user: (state) => state.authUser
    }
  
})
