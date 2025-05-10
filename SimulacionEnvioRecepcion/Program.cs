using System;
using System.Text;
using System.Security.Cryptography;
using ClaveSimetricaClass;
using ClaveAsimetricaClass;

namespace SimuladorEnvioRecepcion
{
    class Program
    {   
        static string UserName;
        static string SecurePass;  
        static ClaveAsimetrica Emisor = new ClaveAsimetrica();
        static ClaveAsimetrica Receptor = new ClaveAsimetrica();
        static ClaveSimetrica ClaveSimetricaEmisor = new ClaveSimetrica();
        static ClaveSimetrica ClaveSimetricaReceptor = new ClaveSimetrica();

        static string TextoAEnviar = "Me he dado cuenta que incluso las personas que dicen que todo está predestinado y que no podemos hacer nada para cambiar nuestro destino igual miran antes de cruzar la calle. Stephen Hawking.";
        
        static void Main(string[] args)
        {

            /****PARTE 1****/
            //Login / Registro
            Console.WriteLine("¿Deseas registrarte? (S/N)");
            string registro = Console.ReadLine();

            if (registro =="S")
            {
                //Realizar registro del cliente
                Registro();                
            }

            //Realizar login
            bool login = Login();

            /***FIN PARTE 1***/

            if (login)
            {                  
                byte[] TextoAEnviar_Bytes = Encoding.UTF8.GetBytes(TextoAEnviar); 
                Console.WriteLine("Texto a enviar bytes: {0}", BytesToStringHex(TextoAEnviar_Bytes));    
                
                //LADO EMISOR

                //Firmar mensaje


                //Cifrar mensaje con la clave simétrica


                //Cifrar clave simétrica con la clave pública del receptor

                //LADO RECEPTOR

                //Descifrar clave simétrica

                
                //Descifrar clave simétrica
 

                //Descifrar mensaje con la clave simétrica


                //Comprobar firma

            }
        }

        public static void Registro()
        {
            Console.WriteLine("Indica tu nombre de usuario:");
            UserName = Console.ReadLine();
            //Una vez obtenido el nombre de usuario lo guardamos en la variable UserName y este ya no cambiará 

            Console.WriteLine("Indica tu password:");
            string passwordRegister = Console.ReadLine();
            //Una vez obtenido el passoword de registro debemos tratarlo como es debido para almacenarlo correctamente a la variable SecurePass

            /***PARTE 1***/
            /*Añadir el código para poder almacenar el password de manera segura*/

            // Generar un salt aleatorio
            byte[] salt = GenerateSalt();

            // Combinar el password con el salt
            string hashedPassword = HashPassword(passwordRegister, salt);

            // Guardar salt para su posterior logín
            SecurePass = hashedPassword;
        }

        public static bool Login()
        {
            bool auxlogin = false;
            do
            {
                Console.WriteLine("Acceso a la aplicación");
                Console.WriteLine("Usuario: ");
                string userName = Console.ReadLine();

                Console.WriteLine("Password: ");
                string Password = Console.ReadLine();

                /***PARTE 1***/
                /*Modificar esta parte para que el login se haga teniendo en cuenta que el registro se realizó con SHA512 y salt*/

                byte[] salt = GetSalt(userName);

                if (salt == null)
                {
                    Console.WriteLine("Usuario no encontrado.");
                    return false;
                }

                // Calcular el hash del password introducido con el mismo salt
                string hashedPassword = HashPassword(password, salt);

                // Obtener el hash del password guardado para el usuario
                string storeHashedPassword = GetStoredPasswordHash(userName);

                // Comparar los hashes
                bool loginSucessful = hashedPassword == storeHashedPassword;

                if (loginSucessful)
                {
                    Console.WriteLine("Login correcto.");
                } else 
                {
                    Console.WriteLine("Nombre de usuario o password incorrecto.");
                }
                return loginSucessful;

            } while (!auxlogin);

            return auxlogin;
        }

        public static byte[] GenerateSalt() 
        {
            // Generar un salt aleatorio 
            byte[] salt = new byte[16];
            using(RNGCryptoServiceProvider rngCSP = new RNGCryptoServiceProvider()) 
            {
                rngCSP.GetBytes(salt);
            }
            return salt;
        }

        public static string HashPassword(string password, byte[] salt)
        {
            // Combinar el password con el salt
            using(SHA512 sha512 = sha512.Create())
            {
                // Concatenar el password con el salt
                byte[] combinedBytes = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();

                // Calcular el hash
                byte[] hashBytes = sha512.ComputeHash(combinedBytes);

                // Convertir el hash en una cadena hexadecimal
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        static string BytesToStringHex(byte[] result)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach(byte b in result)
                stringBuilder.AppendFormat("{0:x2}", b);
            return stringBuilder.ToString();
        }        
    }
}
