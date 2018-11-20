
#NLP

## NLP Engine for Zevere

A NLP(Natural Language Processing) engine and web API for Zevere
[![Docker Build]](https://travis-ci.org/Zevere/NLP-Python/builds/451211526)


## Table of Contents
- [NLP](#NLP)
    - [A NLP(Natural Language Processing) engine and web API for Zevere.](# NLP Engine for Zevere)
    - [Table of Contents](#table-of-contents)
    - [Overview](#overview)
    - [Development](#development)
        - [Install Dependencies](#install-dependencies)
        - [Running in Development](#running-in-development)
        - [To test the NLP](#to-test-the-NLP)


## overview

This NLP code are written based on the code given by Professor Jeff Heaton, Ph.D., is a data scientist, an adjunct instructor for the Sever Institute at Washington University, and the author of several books about artificial intelligence. 

##Development
This project makes use of experimental Python code. It is recommended you use an IDE or editor that supports Python.

##Install Dependencies
The following packages are needed to be installed:

conda install scipy
pip install --upgrade sklearn

pip install --upgrade pandas

pip install --upgrade pandas-datareader

pip install --upgrade matplotlib

pip install --upgrade pillow

pip install --upgrade requests

pip install --upgrade h5py

pip install tensorflow==1.2.1

pip install keras==2.0.6


## Running in Development 
Use the following docker run command to run the NLP

docker run --rm -it -p 5000:5000/tcp zevere/nlp:latest

## To test the NLP outcome run NLP_API_CALL.html file from any web application server and ask question.
There are only 3 question available now.
1. What is task today?
2. What is task tomorrow?
3. What was task yesterday?
