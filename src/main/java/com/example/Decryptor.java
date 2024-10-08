package com.example;

import javax.crypto.Cipher;
import javax.crypto.spec.SecretKeySpec;
import javax.crypto.spec.IvParameterSpec;
import java.util.Base64;

public class Decryptor {
    private static final String KEY = "chiave_segreta_32_byte"; // Chiave AES-256 deve essere di 32 byte
    private static final String INIT_VECTOR = "iv_16byte_length"; // IV deve essere di 16 byte

    public static String decrypt(String encrypted) {
        try {
            IvParameterSpec iv = new IvParameterSpec(INIT_VECTOR.getBytes("UTF-8"));
            SecretKeySpec skeySpec = new SecretKeySpec(KEY.getBytes("UTF-8"), "AES");

            Cipher cipher = Cipher.getInstance("AES/CBC/PKCS5PADDING");
            cipher.init(Cipher.DECRYPT_MODE, skeySpec, iv);

            byte[] original = cipher.doFinal(Base64.getDecoder().decode(encrypted));

            return new String(original);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return null;
    }

    public static void main(String[] args) {
        String encryptedData = "qui_va_la_stringa_crittografata"; // Sostituisci con i dati crittografati
        System.out.println("Dato decrittografato: " + decrypt(encryptedData));
    }
}