const amqp = require('amqplib/callback_api');
const EmailService = require('./src/services/EmailServices');
const dotenv = require('dotenv');

  
dotenv.config();

amqp.connect('amqp://localhost',(connError, connection) =>{
    if(connError){
        throw connError;
    }

    connection.createChannel((channelError, channel)=>{
        if(channelError){
            throw channelError;
        }
        
        const QUEUE = 'email_new_user';
        channel.assertQueue(QUEUE);

        channel.consume(QUEUE, (msg)=>{

        var emailData = msg.content;
        
        EmailService.sendEmail(emailData);
            
        },  
        {
            noAck: true
          })

        console.log(`Message send ${QUEUE}`);
    })
});