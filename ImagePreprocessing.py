import skimage
import biteai


def preprocess(image, coordinate):

    # define crop widths
    crop_width = 100
    crop_height = 100

    # checking for valid image dimensions
    assert image.shape[0] >= coordinate[0] + crop_width
    assert image.shape[1] >= coordinate[1] + crop_height

    assert coordinate[0] - crop_width >= 0
    assert coordinate[1] - crop_height >= 0

    # crop image
    crop = image[coordinate[0]-crop_width:coordinate[0]+crop_width,
                 coordinate[1]-crop_height:coordinate[1]+crop_height]

    return crop


def call_food_to_image_api(image):

    user = biteai.User(token='[5ced68e643ecc02d1bc07b557c941be2aa90976b]')
    meals = user.meals.list()


