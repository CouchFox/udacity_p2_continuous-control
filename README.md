[//]: # (Image References)

[image1]: https://user-images.githubusercontent.com/10624937/43851024-320ba930-9aff-11e8-8493-ee547c6af349.gif "Trained Agent"


# Deep Reinforcement Learning Nanodegree
## Project 2 Readme

![Trained Agents][image1]

This repository contains material related to Udacity's [Deep Reinforcement Learning Nanodegree] (https://www.udacity.com/course/deep-reinforcement-learning-nanodegree--nd893) program Project 2 .  


### Project Details
#### Part 1
The goal of this project was to train a DRL-Networt for an environment with a double-jointed arm to learn to stay at a moving target locations. A reward of +0.1 is provided for each step that the agent's hand is in the goal location.

The observation space consists of 33 variables corresponding to position, rotation, velocity, and angular velocities of the arm. Each action is a vector with four numbers, corresponding to torque applicable to two joints. Every entry in the action vector should be a number between -1 and 1.

I chose to run the environment with 20 agents.

The environment is considered solved, when the average (over 100 episodes) of the average score of those 20 agents is at least +30.


#### Part 2
As an optional part I tried to train an agent with 4 legs to walk. To do this, I used the Unity ml-agents kit (see Resources). The first attempt (see animated gif at the top) was abit clunky. When looking for more ideas on how to get better results, I stumbled upon Unitys new PuppoDemo. It features a Corgi which learns to walk and reach a target location (much like Crawler but much cuter).


### Getting Started
To install the code for Project 2 of the Udacity Deep Reinforcement Learning Nanodegree, follow the instructions here: https://github.com/udacity/deep-reinforcement-learning

I chose to install the code locally on an Ubuntu 16.04 with an NVidia 1080 Ti graphics card. To setup tensorflow with gpu, cuda and cuDNN I followed this instructions:
https://medium.com/@zhanwenchen/install-cuda-and-cudnn-for-tensorflow-gpu-on-ubuntu-79306e4ac04e

To install this repo just git clone it and copy the content (incl. the two folders) to the deep-reinforcement-learning folder from above.

then use conda with the provided environment.yml to install the dependencies:
```
conda env create -f environment.yml
```

To use the code for the optional part, download the unity ml-agent kit and add it to the PuppoDemo/Assets folder. Then train it with the code given in https://github.com/Unity-Technologies/ml-agents/ .

### Instructions
#### Part 1
**To run the code:** start the jupyter notebook called Continuous_Control.ipynb and run the cells. Skip step 3 if you're not interested in random actions (or can't display them).
#### Part 2 (optional)
Copy the ml-agents [subfolder](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Installation.md) into the PuppoDemo Folder

**To see the result:** open the Unity Environment and make sure the GameObject Academy/DogBrain is set to internal and there is a model set (from Assets/Corgi/TFModel)

**To train for yourself:** open the Unity Environment and set the GameObject Academy/DogBrain to external. Then start the ml-agent training process like so:
```
mlagents-learn config/trainer_config.yaml --run-id=husky-01 --train
```
**To see tensorboard output:** type the following in the same directory while trainging and see the result on http://localhost:6006
```
tensorboard --logdir=summaries
```

### Resources
- Unity's ML-Agents: https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Installation.md
- Unity's PuppoDemo: https://blogs.unity3d.com/2018/10/02/puppo-the-corgi-cuteness-overload-with-the-unity-ml-agents-toolkit/.
