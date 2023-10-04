<template>
  <v-card  class="container" elevation="3" outlined color="#fff9db" max-width="550">
    <openSlotsDialog :shift="shifts" :kontor-nr-store="kontorNrStore" :uketall=uketall :open=open ></openSlotsDialog>
    <v-card-title class="grid-item tittel-timer">Total hours this week: {{ antallTimerJobbet.toFixed(1) }}</v-card-title>
      <v-card-actions class="grid-item">
      <v-btn color="#f5d066" @click="minus">-</v-btn>
      <v-card-title class="grid-item uketall">Week {{ uketall }}</v-card-title>
      <v-btn color="#f5d066" @click="pluss">+</v-btn>
    </v-card-actions>
    <v-card-text class="custom-hover">
      <v-container fluid>
        <v-row justify="center">
          <v-col cols="12" sm="8" md="6">
            <span class="text--center text--thicker text--bigger hover--text"  @click="endreOpen">Check hours</span>
          </v-col>
        </v-row>
      </v-container>
    </v-card-text>
    <v-card-text>{{ msg }}</v-card-text>
  </v-card>
</template>

<script>
import axios from 'axios';
import openSlotsDialog from './openSlotsDialog.vue';

export default {
  components: {openSlotsDialog},
  mounted () {
    this.getTotaltAntallTimerJobbet(this.uketall);
    this.uketall++;
    this.uketall--;
  },
  methods: {
    pluss() {
      this.uketall++;
    },
    endreOpen(){
        this.open = !this.open;
      },
    minus() {
      this.uketall--;
    },
    getTotaltAntallTimerJobbet(i) {
      var link = "/statistics/alle/" + i + "/" + this.kontorNrStore;
      axios.get(link)
        .then(response => {
          this.antallTimerJobbet = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    },
  },
  watch: {
    uketall() {
      this.$emit("uketall", this.uketall);
      var link = "/skift/" + this.kontorNrStore + "/" + this.uketall;
        axios.get(link)
      .then(response => {
        this.shifts = response.data;
      })
      .catch(error => {
        console.log(error);
      });
      this.getTotaltAntallTimerJobbet(this.uketall);
    },

    shifts(){
      if (this.shifts.length == 0){
        this.msg = "No unmanned slots this week"
      }
      else {
        this.msg = "You have unmanned slots this"
      }
    }
  },
  props: {
    kontorNrStore: Number
  },

  data() {
    return {
      uketall: 20,
      antallTimerJobbet: 0,
      open: false,
      shifts: [],
      msg: ""
    };
  },
};
</script>

<style scoped>
.grid-container {
  display:grid;
  grid-template-columns: auto auto auto;
  justify-content: center;
  align-items: center;
  padding: 24px;
  background-color: #fdfdfd;
}

.tittel-timer {
  grid-column-start: 1;
  grid-column-end: 4;
  text-align: center;
  color: #4c4c4c;
}

.timer {
  grid-column-start: 1;
  grid-column-end: 4;
  text-align: center;
  font-size: 36px;
  font-weight: bold;
  color: #f5d066;
}

.uketall {
  font-size: 24px;
  font-weight: bold;
  color: #4c4c4c;
}

.v-btn {
  font-size: 24px;
  font-weight: bold;
  height: 64px;
  width: 64px;
  border-radius: 32px;
}

.v-btn:hover {
  background-color: #f5d066;
  color: #ffffff;
}

.v-card {
  border-radius: 6px;
  border: none;
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
}
.custom-hover:hover {
  background-color: #f5d066;
}

.text--thicker {
  font-weight: bold;
}

.text--bigger {
  font-size: 30px;
  text-align: center;
}



</style>

  