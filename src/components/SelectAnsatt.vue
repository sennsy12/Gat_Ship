<template>
    <v-container fluid>
      <v-row align="center">
        <v-col cols="6">
          <v-select
            v-model="select"
            :hint="`${select.name}`"
            :items="items"
            item-text="name"
            item-value="ansatt_ansattNr"
            label="Velg Ansatt"
            persistent-hint
            return-object
            single-line
          ></v-select>
        </v-col>
      </v-row>
    </v-container>
  </template>

<script>
import axios from 'axios';
  export default {
    data () {
      return {
        select: {},
        items: [],
      }
    },

    props: {
      uketall: Number,
      kontorNr: Number
    },

    mounted () {
      var link = "statistics/" + this.uketall + "/" + this.kontorNr;
        axios.get(link)
      .then(response => {
        this.items = response.data;
      })
      .catch(error => {
        console.log(error);
      });
      },

      watch: {
        select(){
            this.$emit("valgtAnsattNr", this.select);
            console.log(this.select);
            console.log(this.uketall);
        }
      }
  }
</script>