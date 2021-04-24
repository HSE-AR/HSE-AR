FROM ubuntu:18.04

# Create program install folder
ENV PROGAM_PATH /home/floorplan_to_blender
RUN mkdir -p ${PROGAM_PATH}

# Add our program
ADD ./ ${PROGAM_PATH}/

# Install needed programs
RUN apt-get update && \
	apt-get install -y \
	wget \
	bzip2 \
	libfreetype6 \
	libgl1-mesa-dev \
	libglu1-mesa \
	libxi6 \
    libsm6 \
	xz-utils \
	libxrender1 \
    git \
    nano  \
	libxext6 && \
    apt-get install --no-install-recommends -y \
    python3.8 python3-pip python3.8-dev && \
	apt-get -y autoremove && \
	rm -rf /var/lib/apt/lists/*

# Install blender
ENV BLENDER_PATH /usr/local/blender/blender
ENV BLENDER_MAJOR 2.92
ENV BLENDER_VERSION 2.92.0
ENV BLENDER_BZ2_URL https://www.blender.org/download/Blender$BLENDER_MAJOR/blender-$BLENDER_VERSION-linux64.tar.xz/
# Download and install Blender
RUN wget https://mirror.clarkson.edu/blender/release/Blender2.92/blender-2.92.0-linux64.tar.xz \ 
	&& tar -xvf blender-2.92.0-linux64.tar.xz --strip-components=1 -C /bin \ 
	&& rm -rf blender-2.92.0-linux64.tar.xz \ 
	&& rm -rf blender-2.92.0-linux64 

# Download the Python source since it is not bundled with Blender
RUN wget https://www.python.org/ftp/python/3.7.0/Python-3.7.0.tgz \ 
	&& tar -xzf Python-3.7.0.tgz \ 
	&& cp -r Python-3.7.0/Include/* $BLENDER_PATH/python/include/python3.7m/ \ 
	&& rm -rf Python-3.7.0.tgz \ 
	&& rm -rf Python-3.7.0 

# Blender comes with a super outdated version of numpy (which is needed for matplotlib / opencv) so override it with a modern one
RUN rm -rf ${BLENDER_PATH}/python/lib/python3.7/site-packages/numpy 

RUN pip3 install --upgrade setuptools
RUN pip3 install -r ${PROGAM_PATH}/requirements.txt
RUN pip3 install -r ${PROGAM_PATH}/Docs/requirements.txt
RUN pip3 install -r ${PROGAM_PATH}/Development\ Center/requirements.txt

# Volume to share images and get data after execution
VOLUME ${PROGAM_PATH}/Images
VOLUME ${PROGAM_PATH}/Target
VOLUME ${PROGAM_PATH}/Data

# Default volume Blender
VOLUME /media 

# Set default blender path in config file (doing this twice is a little hax!)
RUN sed -i 's#blender_installation_path=.*#blender_installation_path='"${BLENDER_PATH}"'#g' ${PROGAM_PATH}/config.ini
RUN sed -i 's#blender_installation_path=.*#blender_installation_path='"${BLENDER_PATH}"'#g' ${PROGAM_PATH}/config.ini

WORKDIR ${PROGAM_PATH}
