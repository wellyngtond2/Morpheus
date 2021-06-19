
var nodemailer = require('nodemailer');

const sendEmail =  function(EmailRequest){
  
  const transporter = nodemailer.createTransport({
    host: process.env.EMAIL_SMTP,
    port: process.env.EMAIL_PORT,
    auth: {
        user:process.env.EMAIL_USER,
        pass: process.env.EMAIL_PASS  
    }
  });

    transporter.verify(function(error, success) {
    if (error) {
      console.log(error);
    } else {
      console.log("Server is ready to take our messages");
    }
  });

  console.log(`try to send email ${EmailRequest}`);

  transporter.sendMail({})
  .then(info => {
    console.log('success to send email', info);
  })
  .catch(error => {
    console.log('fail to try send: ',error)
    throw error;
  });

}

module.exports = { sendEmail };