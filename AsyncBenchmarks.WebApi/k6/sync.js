import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
  stages: [
    { duration: '10s', target: 500 },  // Ramp-up to 10 VUs over 30 seconds
    //{ duration: '1m', target: 50 },   // Hold at 50 VUs for 1 minute
    //{ duration: '20s', target: 0 },   // Ramp-down to 0 VUs over 20 seconds
  ],
};


export default function() {
  http.get('http://localhost:5000/sync');
  //sleep(1);
}
