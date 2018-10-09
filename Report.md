[//]: # (Image References)

[image1]: husky2.gif "Trained Husky Agents"
[image2]: tensorboard.png "Tensorboard Summary"
[image3]: dog-walk.gif "Clunky RoboDog"
[image4]: result.png "Result"
[image5]: plot.png "Plot of Rewards"

# Deep Reinforcement Learning Nanodegree - Project 2 Report
The goal of this project was to train a DRL-Networt for an environment with a double-jointed arm to learn to stay at a moving target locations. A reward of +0.1 is provided for each step that the agent's hand is in the goal location.

The observation space consists of 33 variables corresponding to position, rotation, velocity, and angular velocities of the arm. Each action is a vector with four numbers, corresponding to torque applicable to two joints. Every entry in the action vector should be a number between -1 and 1.

I chose to run the environment with 20 agents.

The environment is considered solved, when the average (over 100 episodes) of the average score of those 20 agents is at least +30.

## Learning Algorithm
For this Project the DDPG (Deep Deterministic Policy Gradient) is used to solve the environment.
An advantage of DDPG over say DQN is that it doesn't rely on discretisation. Instead it can directly deal with continuous inputs. DDPG is an actor-critic architecture with the actor to be used to learn the parameter theta for the policy function. The critic is then used to evaluate the policy function of the actor.

The code is based on the agent and model from the ddpg-bipedal source for the ai gym. Both network models for the actor and the critic consist of simple fully connected layers with 2 hidden units each.
The modifications consisted of the following changes:
- instead of a gym environment, the unity environment had to be used. (see comment in notebook for alterations)
- the model for the critic and actor were chosen smaller for this environment:
 - actor: 256x256
 - critic: 128x128
- I added a batchnorm layer to the actor and critic network to make it more stable
- the hyperparameters were chosen as follows:
```
BUFFER_SIZE = int(1e6)  # replay buffer size - didn't need change
BATCH_SIZE = 1024       # minibatch size before 128 - seems to run more stable when higher
GAMMA = 0.99            # discount factor - no change
TAU = 1e-3              # for soft update of target parameters - no change
LR_ACTOR = 1e-3         # learning rate of the actor - worked better with 1e-3 than 1e-4 in ddpg-bipedal
LR_CRITIC = 1e-3        # learning rate of the critic - no change
WEIGHT_DECAY = 0.000    # L2 weight decay - set to zero as in ddpg-pendulum
```
- the code of the DDPGAgent had to be changed to allow for more than one agent:
```
for state, action, reward, next_state, done in zip(states, actions, rewards, next_states, dones):
            self.memory.add(state, action, reward, next_state, done)
```
- also the learning part was skipped in the step function and extracted to its own function ```learnexperiences(self, repeat_learning=10)``` for more control over when and how often to be used


## Plot of Rewards
Before adding batchnorm to both actor and critic, the model seemed highly unstable. Also the chosen seed made a big difference.

Here you can see the last training episodes:
![Results][image4]


And here is the plot of rewards while training:
![Plot of Rewards][image5]

## Additional Work
Instead of attempting to solve the Crawler-Environment, I tried to train a quadruped to walk with the unity ml-agents kit. I based it no the official Unity Crawler environment and changed the joints to match my blender model. (this attempt is not included in the repo).

The first tests were abit clunky:
![Clunky RoboDog][image3]

When scouring the net for better approaches I came across the newly published PuppoDemo by Unity. They use PPO as a learning algorithm to train a Corgi to walk (or hop) towards a target stick (see [readme](README.md) for links and resources) .

However I found the provided Corgi spent a lot of time dragging its belly in the mud and therefore I chose to make the following alterations:
- changed the Corgi into a Husky with longer legs and different color
- give rewards for having its feet high up: (probably could be done more elegantely)
```
void RewardLiftingFeet() {
    float distFoot0 = (leg0_upper.GetChild(0).position - leg0_lower.GetChild(0).position).sqrMagnitude;
    float distFoot1 = (leg1_upper.GetChild(0).position - leg1_lower.GetChild(0).position).sqrMagnitude;
    float distFoot2 = (leg2_upper.GetChild(0).position - leg2_lower.GetChild(0).position).sqrMagnitude;
    float distFoot3 = (leg3_upper.GetChild(0).position - leg3_lower.GetChild(0).position).sqrMagnitude;
    float totalDist = distFoot0 + distFoot1 + distFoot2 + distFoot3;

    AddReward(-0.001f * totalDist);
}
```
- give rewards for its body being as high as can be
```
void RewardHighBody() {
    AddReward(transform.GetChild(0).position.y * 0.001f);
}
```
- give penalty points for the body and upper legs touching the ground (called from the ground object when in contact with a gameobject tagged as 'body')
```
public void BodyTouchingGround() {
    AddReward(-0.01f);
}
```

The suggested hyperparameters worked nicely out of the box:
```
DogBrain:
  normalize: true
  num_epoch: 3
  time_horizon: 1000
  batch_size: 2048
  buffer_size: 20480
  gamma: 0.995
  max_steps: 2e6
  summary_freq: 3000
  num_layers: 3
  hidden_units: 512
```
The training went relatively smoothly, as shown on tensorboard:

![Tensorboard Summary][image2]


The result you can see here:

![Trained Husky Agents][image1]




## Ideas for Future Work
- solve the project with ppo instead of ddpg
- adapt the code learnt in the udacity lessons to tensorflow
- find rewards (or other means) to train a quadruped to walk in the way Boston Dynamics SpotMini does.
- create the model for the quadruped in blender and use it in unity
- use the trained agent to steer the motion of a robot
