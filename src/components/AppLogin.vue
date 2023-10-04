<template>
     
  <v-app>  
    <nav>
    <v-app-bar color="orange" dark app flat>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title class="text-uppercase">
        <span class="font-weight-light">Gat</span>
        <span>Ship</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
     
  
      
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" dark app class="grey darken-4">
      <v-layout column align-center>
        <v-flex class="mt-5">
          <transition>
            <v-avatar size="100" v-show="drawer">
              <img src="../assets/gatship_logo.svg" alt="" /> </v-avatar
          ></transition>
          <!-- <p class="white--text subheading mt-1 text-center">Gatship</p>  -->
        </v-flex>
        <v-flex class="mt-4 mb-4"> </v-flex>
      </v-layout>
     
    
    </v-navigation-drawer>
  </nav> 
  
    <div class="backgruond"></div>
    <v-main class="v-flex  justify-center align-center">
      <v-col cols="10" lg="6" class="mx-auto">
        <v-card class="pa-4" >
          <div class="text-center">
            <h2 class="indigo--text">Sign In</h2>
          </div>
          <v-form @submit.prevent="submitHandler" ref="form">
            <v-card-text>
              <v-text-field
                v-model="brukernavn"
                :rules="emailRules"
                type="email"
                label="Email"
                placeholder="Email"
                prepend-inner-icon="mdi-account"
                required
              />
              <v-text-field
                v-model="passord"
                :rules="passwordRules"
                :type="passwordShow ? 'text' : 'password'"
                label="Password"
                placeholder="Password"
                prepend-inner-icon="mdi-key"
                :append-icon="passwordShow ? 'mdi-eye' : 'mdi-eye-off'"
                @click:append="passwordShow = !passwordShow"
                required
              />
              <v-switch label="Remember me" color="indigo"></v-switch>
            </v-card-text>
            <v-card-actions class="justify-center">
              <v-btn
                @click="show = !show"
                :loading="loading"
                type="submit"
                color="indigo"
              >
                <span class="white--text px-8">Login</span>
                <transition>
                  <div v-if="show" class="loading-animation">
                    <div class="spinner">
                      <img
                        src="gear.png"
                        alt="loading..."
                        style="width: 25px; height: 25px"
                      />
                    </div>
                  </div>
                </transition>
              </v-btn>
            </v-card-actions>
          </v-form>
        </v-card>
      </v-col>
      
    </v-main>

  
  </v-app>
</template>

<script>
import { useAuthStore } from "@/store/auth";
import AppNavbar from "../components/AppNavbar.vue";
export default {
  comments:{AppNavbar},
  name: "App",

  setup() {
    const store = useAuthStore();

    return { store };
  },


  data: () => ({
    drawer: true,
    loading: false,
    snackbar: false,
    passwordShow: false,
    brukernavn: "",
    emailRules: [
      (v) => !!v || "E-mail is required",
      (v) => /.+@.+\..+/.test(v) || "E-mail must be valid",
    ],
    passord: "",
    passwordRules: [
      (v) => !!v || "Password is required",
      (v) => (v && v.length >= 6) || "Password must be 6  characters or more!",
    ],

    show: false,
  }),

  methods: {
    async submitHandler() {
      console.log(this.brukernavn)
      try {
        const credentials = {
          brukernavn: this.brukernavn,
          passord: this.passord
        };
        console.log(credentials)
        await this.store.submitHandler(credentials);
        this.$router.push('/profileView');
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.snackbar = true;
        }, 3000);
      } catch (error) {
        console.error(error)
      }
  },

  },
};
</script>
<style>
.backgruond {
  height: 100px;
  width: 100%;
  display: block;
  position: fixed;
  top: 0;
}

.loading-animation {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 20vh;
}

.spinner {
  animation: spin 2.5s linear infinite;
  display: inline-block;
}

@keyframes spin {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}
.border {
  border-left: 4px solid #fd9800;
}

.v-enter-active {
  animation: bounce-in-right 2s ease infinite;
}

.v-leave-active {
  animation: bounceIn 2s reverse;
}
@keyframes bounce-in-right {
  0% {
    opacity: 0;
    transform: translateX(2000px);
  }
  60% {
    opacity: 1;
    transform: translateX(-30px);
  }
  80% {
    transform: translateX(10px);
  }
  100% {
    transform: translateX(0);
  }
}
</style>
