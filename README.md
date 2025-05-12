# ICB0009-UF1-PR01

### 1. **Registro y Autenticación**

El usuario puede registrarse con un nombre de usuario y una contraseña. Durante este proceso:

* Se genera un **salt** aleatorio.
* Se combina la contraseña con el salt y se aplica **SHA-512** para obtener un hash seguro.
* Se almacena el hash como la contraseña segura del usuario.

Para el  **login** , se recupera el salt, se aplica nuevamente **SHA-512** con la contraseña ingresada y se compara con el hash almacenado.

### 2. **Conversión del mensaje a bytes**

El mensaje de texto que el emisor quiere enviar se convierte en un **array de bytes** utilizando `Encoding.UTF8.GetBytes()`.

## **Lado Emisor**

1. **Firma del mensaje**
   * Se aplica una firma digital al mensaje utilizando la clave privada del **Emisor** (`Emisor.Firmar(TextoAEnviar_Bytes)`).
   * Esta firma permite al receptor verificar que el mensaje proviene realmente del emisor y no ha sido alterado.
2. **Cifrado del mensaje con clave simétrica**
   * Se cifra el mensaje utilizando una **clave simétrica** (`ClaveSimetricaEmisor.Cifrar(TextoAEnviar_Bytes)`).
   * El cifrado simétrico es rápido y eficiente, pero requiere que el receptor tenga la misma clave.
3. **Cifrado de la clave simétrica con clave pública**
   * La clave simétrica utilizada en el cifrado anterior es cifrada con la **clave pública** del receptor (`Receptor.Cifrar(ClaveSimetricaEmisor.Clave)`).
   * Esto asegura que solo el receptor podrá descifrar la clave simétrica y, por ende, el mensaje.

## **Lado Receptor**

1. **Descifrado de la clave simétrica**
   * Se utiliza la **clave privada** del receptor para descifrar la clave simétrica (`Receptor.Descifrar(claveSimetricaCifrada)`).
   * Una vez recuperada la clave simétrica, el receptor podrá descifrar el mensaje.
2. **Descifrado del mensaje**
   * Con la clave simétrica descifrada, el receptor puede ahora descifrar el mensaje (`ClaveSimetricaReceptor.Descifrar(mensajeCifrado)`).
   * Se obtiene el mensaje original en bytes.
3. **Verificación de la firma**
   * Para verificar la autenticidad del mensaje, el receptor usa la clave pública del emisor (`Receptor.Verificar(mensajeFirmado)`).
   * Si el mensaje original coincide con el mensaje verificado, la firma es **válida** y el mensaje es auténtico.

**Explica el mecanismo de Registro / Login utilizado (máximo 5 líneas)**

En el registro, el usuario introduce su nombre y contraseña, que se combina con un **salt** aleatorio y se cifra con **SHA-512** antes de almacenarse de forma segura. En el  **login** , el sistema recupera el salt del usuario, aplica SHA-512 a la contraseña ingresada y compara el resultado con el hash guardado. Si coinciden, el acceso es exitoso; de lo contrario, se deniega.

**Pregunta:**

**Una vez
realizada la práctica, ¿crees que alguno de los métodos programado en la clase
asimétrica se podría eliminar por carecer de una utilidad real?**

* **Firmar()** → Necesario para garantizar autenticidad e integridad.
* **Verificar()** → Indispensable para comprobar la validez de la firma.
* **Cifrar()** → Utilizado para cifrar la clave simétrica con la clave pública del receptor.
* **Descifrar()** → Necesario para recuperar la clave simétrica usando la clave privada del receptor.

Si la firma del mensaje no es necessaria en la aplicación, se podría eliminar Firmar() y Verificar() aunque quitas autenticidad. También se podrían fusionar los métodos Cifrar() y Descifrar().
