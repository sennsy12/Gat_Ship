
<template>
    <div class="knapp">
        <v-btn @click="loadItems" >Last inn ansatte fra backend</v-btn>
        <div v-for="r in results" :key="r.ansattNr">
            <h2>{{ r.ansattNr }} - {{ r.fornavn + " " + r.etternavn }}</h2>
            <p>{{"Tlf: " + r.telefonNr }}</p>

        </div>
    </div>
</template>


<script>
import axios from 'axios';
  export default {
    name: 'ShowAnsattTimer',
    data: () => ({
        results: []
    }),
    methods: {
        loadItems(){
         axios.get('/ansatte')
         .then(response => this.results= response.data)
        }
    }
  } 
</script>
<style scoped>
    .knapp {
        text-align: center;
    }
</style>