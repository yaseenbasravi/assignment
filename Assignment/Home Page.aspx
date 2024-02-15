<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home Page.aspx.cs" Inherits="Assignment.Home_Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login and Registration page</title>
    <link rel="stylesheet" href="login.css"/>
</head>
<body>
    <div class ="container">
        <div class="form-box">
            <!--registration-->
            <div class="register-box hidden">
                <h1>register</h1>
                <input type="text" placeholder="username" required>
                <input type="tel" placeholder="phone number" pattern="[0-9]{3}-[0-9]{5} [0-9]{4}" required>
                <input type="text" placeholder="house address" required>
                <input type="email" placeholder="email" required>
                <input type="password" placeholder="password" required>
                <input type="password" placeholder="re-enter password" required>
                <button>register</button>
            </div>
        
            <!--login-->
            <div class="login-box">
                <h1>login</h1>
                <input type="text" placeholder="username" required>
                <input type="password" placeholder="password" required>
                <button>login</button>
            </div> 
        </div>
         <div class="con-box left">
            <h2>WELCOME TO OUR <span>RECIPE SCHOOL</span></h2>
            <p>LEARN A NEW <span>RECIPE</span> NOW</p>
            <img src="WhatsApp Image 2023-12-10 at 6.57.02 PM.jpeg" alt="">
            <p>ALREADY HAVE AN ACCOUNT</p>
            <button id="login"> proceed to login </button>
        </div>
            
        <div class="con-box right">
            <h2>WELCOME TO OUR <span>RECIPE SCHOOL</span></h2>
            <p>FIND A <span>RECIPE</span> NOW</p>
            <img src="823214.png" alt="">
            <p>DON'T HAVE AN ACCOUNT ?</p>
            <button id="register"> proceed to register </button>
        </div>

        

    </div>
    <script>
        //要操作到的元素
        let login=document.getElementById('login');
        let register=document.getElementById('register');
        let form_box=document.getElementsByClassName('form-box')[0];
        let register_box=document.getElementsByClassName('register-box')[0];
        let login_box=document.getElementsByClassName('login-box')[0];
        //去注册按钮点击事件
        register.addEventListener('click',()=>{
            form_box.style.transform='translateX(80%)';
            login_box.classList.add('hidden');
            register_box.classList.remove('hidden');
        })
        //去登录按钮点击事件
        login.addEventListener('click',()=>{
            form_box.style.transform='translateX(0%)';
            register_box.classList.add('hidden');
            login_box.classList.remove('hidden');
        })
    </script>
</body>
</html>
