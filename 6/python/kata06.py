#!/usr/bin/env python
# -*- coding: utf-8 -*-

import os
from urllib2 import urlopen

FILE_NAME = '/wordList.txt'
DIR_PATH = os.path.dirname(os.path.realpath(__file__))


class Anagramer:
    wordList = []

    def __init__(self):
        print 'Initializing Anagramer ... '

        # Check if the word list is loaded
        if os.path.isfile(DIR_PATH + FILE_NAME):
            print 'Reading stored word list'
            self.read_file()
        else:
            print 'Downloading word list'
            self.download_file()

        print 'Word list loaded'

    def read_file(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        with open(dir_path + FILE_NAME, 'r') as infile:
            for line in infile:
                self.wordList.append(line.replace('\n', ''))

    def download_file(self):
        urlpath = urlopen('http://codekata.com/data/wordlist.txt')
        raw = urlpath.read().decode('latin-1')
        self.store_file(raw_data=raw)
        self.wordList = raw.split('\n')

    def store_file(self, raw_data):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        with open(dir_path + FILE_NAME, 'w') as outfile:
            for line in raw_data:
                outfile.write(line.encode('UTF-8'))

    def group_words(self, lower=None, upper=None):
        lookup = {}
        i = 0

        for i in range(lower or 0, upper or len(self.wordList)):

            word = self.wordList[i]
            sorted_word = ''.join(sorted(word))

            if sorted_word in lookup:
                lookup[sorted_word].append(word)
            else:
                lookup[sorted_word] = [word]

        # Remove single words
        return {k: v for k, v in lookup.items() if len(v) > 1}


if __name__ == '__main__':
    a = Anagramer()
    anagrams = a.group_words()
    print 'Number of anagrams: %s' % len(anagrams)
