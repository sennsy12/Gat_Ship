<template>
<container class="grid-container">  
  
        <ChooseWeek :kontor-nr-store="kontorNrStore" @uketall="endreUke" class="grid-item choose-week"></ChooseWeek>
        <div class="grid-item" v-for="(ansatt, index) in ansatte" :key="index">
            <EmployeeCards :name=ansatt.name role="test" :min-timer-i-uka="ansatt.minTimerIUka" :max-timer-i-uka="ansatt.maxTimerIUka" :ansatt-nr-color="ansatt.color" :ansatt-nr=ansatt.ansatt_ansattNr :antall-timer-jobbet=ansatt.antallTimerJobbet></EmployeeCards>
        </div>
    
</container>


</template>

<script>
import axios from 'axios';
import EmployeeCards from './EmployeeCards.vue';
import ChooseWeek from './ChooseWeek.vue';

import jwt_decode from 'jwt-decode'

import { useAuthStore } from "@/store/auth";

  export default {
    components: {EmployeeCards, ChooseWeek },
    data () {
      return {
        ansatte: [],
        uketall: 20
      }
    },

    setup() {
    const store = useAuthStore();
    let username = "";
    let navn = "";
    let epost = "";
    let kontorNrStore = ""
   


    if (store.token) {
const decoded = jwt_decode(store.token)
username = decoded.unique_name;
navn = decoded.navn
epost = decoded.epost
kontorNrStore = decoded.kontorNr
        }
    return { store, username, navn, epost, kontorNrStore };
  },

    mounted () {
        this.getAnsatte(this.uketall);
    },

    methods: {
        getAnsatte(i)
        {
            var link = "statistics/" + i + "/" + this.kontorNrStore;
            axios.get(link)
      .then(response => {
        this.ansatte = response.data;
      })
      .catch(error => {
        console.log(error);
      });
      },
      endreUke(i){
        this.uketall = i;
        this.getAnsatte(i);
      }
        }
  }
</script>

<style scoped>
    .grid-container {
        display:grid;
        grid-template-columns: auto auto auto auto;
        column-gap: 10px;
    }
    .choose-week {
        grid-column-start: 1;
        grid-column-end: 1;
    }
</style>