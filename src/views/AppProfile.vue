
<template>
     
  <v-container grid-list-lg>

    <AppNavbar/>
      <v-layout column>
          <v-flex xs12 class="text-xs-center display-1 font-weight-black my-5"
              >My profile</v-flex>
          
      </v-layout>
      <v-layout row wrap class="meal-plans">
          <v-flex xs12 sm12 md4>
              <v-card>
                  <v-responsive>
                      <v-img
                          src="../assets/user2.jpg"
                          height="500px"
                      >
                          <v-container fill-height fluid>
                              <v-layout fill-height>
                                  <v-flex xs12 align-end flexbox>
                                      <span class="headline white--text"
                                          > <div>
                                <h3 v-if="store.token">Hi and Welcome, <br><br><br> Office Name: {{ navn }}, <br> Email: {{ epost }} <br>  telefonNr: {{ telefonNr }}</h3>
                                 <h3 v-if="!user">you are not logged in</h3>
                                  </div></span
                                      >
                                  </v-flex>
                              </v-layout>
                          </v-container>
                      </v-img>
                  </v-responsive>

                  <v-card-text>
                      <div>
                          <p class="headline mb-0" v-if="store.token">Hi and Welcome, Name: {{ navn  }},<br> OfficeNr:{{ kontorNr }} <br> Email: {{ epost }} <br>  telefonNr: {{ telefonNr }}
                          </p>
                 <p v-if="!user">you are not logged in</p>
               
                      </div>
                  </v-card-text>

                 
              </v-card>
          </v-flex>

          <!-- {{store.token.kontorNr}} <br><br><br> KontorNavn:{{ store.token.navn }}, Epost:{{ store.token.epost }} -->
      </v-layout>
  </v-container>
</template>


<script>
import axios from "axios";
import AppNavbar from '@/components/AppNavbar.vue';

import jwt_decode from 'jwt-decode'

import { useAuthStore } from "@/store/auth";
//import AppNavbar from "../components/AppNavbar.vue"
export default {
  components:{AppNavbar,},
  name: "AppProfile",

  setup() {
    const store = useAuthStore();
    let username = "";
    let navn = "";
    let epost = "";
    let telefonNr = "";
    let kontorNr = ""
   


    if (store.token) {
const decoded = jwt_decode(store.token)
username = decoded.unique_name;
navn = decoded.navn
epost = decoded.epost
telefonNr = decoded.telefonNr
kontorNr = decoded.kontorNr
        }
    return { store, username, navn, epost, telefonNr ,kontorNr };
  },

  
  data() {
    return {
      user: this.store.token,
    };
  },
  watch: {
   user(){
    console.log("this is user from watch",this.user);
   // this.store.token = this.user
   },
  

  },
  async created() {
    const response = await axios.get("login/test/" + this.kontorNr);
    console.log(response.data);
    this.user = response.data;
    //this.store.token = response.data
  },

  

};
</script>

<style lang="scss" scoped></style>
