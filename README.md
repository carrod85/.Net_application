## Name: Carlos Rodriguez
## Student code: 201841IVSB
## Uni id: carrod85




### Summary of tasks
**First part** of the tasks has been to create 4 crypto programs.
    1. Cesar
    2. Vigenere
    3. Diffie Hellman
    4. Rsa

For these tasks console has been used for input data, trying to control all variables of input that can crash the program
either by controlling the input or handling the exception at runtime.

No crypto modules has been used and crypto breakers for Diffie Hellman and Rsa
has been implemented.

Since the idea of implementing encryption and decryption on the web App has been carried out,
a feature to check base64 text has been added in Vigenere from the initial features.

An *additional feature* has been developed and is to serialize a number in byte array to be saved
for Diffie Hellman.
It was mentioned in class.

**Second part** of the tasks has been creating a webApp in asp.net framework
where all the logic from the *First part* has been reviewed and separated in functions
for clarity, specially when applying it to controllers

The model for every crypto system has been created as well as the modified "identity user table" which has been 
called AppUser, and the relation 1..M for them. It has been deploy in database through migrations and apply changes
in database.

The controllers were created. Some from-end Javascript code has been added and 'linked' to logic implementation
on controllers, so we can encrypt and decrypt when creating or editing new entries and we can fully use our initial
consoleApp application through Web Application.

The framework comes with security features but as it has been explained, the fetching and posting of data
has to be controlled with claims and queries for the crypto object and It has been done
for every *crud* operation





